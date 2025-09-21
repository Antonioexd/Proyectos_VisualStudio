Public Class Form1
    ' Variables globales
    Private random As New Random()
    Private lancha As Lancha
    Private tiburones As New List(Of Tiburon)
    Private sobrevivientes As New List(Of Sobreviviente)
    Private buque As Buque

    ' Timers
    Private timerJuego As Timer
    Private timerBuque As Timer

    ' Controles
    Private panelSuperior As Panel
    Private lblTiempo As Label
    Private lblPuntaje As Label
    Private lblVidas As Label
    Private lblGasolina As Label

    ' Variables de estado del juego
    Private tiempo As Integer = 0
    Private nivel As Integer = 1
    Private ticksContador As Integer = 0 ' Para controlar el tiempo
    Private gasolinaInicial As Integer = 120 ' Aumentar gasolina inicial

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configuración del formulario
        Me.Text = "Juego de Rescate"
        Me.ClientSize = New Size(800, 600)
        Me.KeyPreview = True ' Permitir capturar teclas directamente en el formulario

        ' Ocultar los PictureBox de almacén
        PB_Lancha.Visible = False
        PB_Tiburon.Visible = False
        PB_Sobreviviente.Visible = False
        PB_Buque.Visible = False

        ' Crear la interfaz superior
        CrearPanelSuperior()

        ' Crear la lancha con más gasolina
        lancha = New Lancha(New Point(400, 500), CType(PB_Lancha.Image.Clone(), Image)) With {
            .Gasolina = gasolinaInicial
        }
        lblGasolina.Text = $"Gasolina: {lancha.Gasolina}" ' Mostrar gasolina inicial
        Me.Controls.Add(lancha.Imagen)

        ' Crear el buque
        Dim posicionBuque As New Point(Me.ClientSize.Width - 100, Me.ClientSize.Height - PB_Buque.Height - 10)
        buque = New Buque(posicionBuque, -5, CType(PB_Buque.Image.Clone(), Image), New Rectangle(0, 0, Me.ClientSize.Width, Me.ClientSize.Height))
        Me.Controls.Add(buque.Imagen)

        ' Configurar los Timers
        ConfigurarTimers()
    End Sub

    ' Método para crear la interfaz superior
    Private Sub CrearPanelSuperior()
        panelSuperior = New Panel With {
            .Dock = DockStyle.Top,
            .Height = 50,
            .BackColor = Color.LightGray
        }
        Me.Controls.Add(panelSuperior)

        lblTiempo = New Label With {
            .Text = "Tiempo: 0",
            .AutoSize = True,
            .Location = New Point(10, 15)
        }
        panelSuperior.Controls.Add(lblTiempo)

        lblPuntaje = New Label With {
            .Text = "Puntaje: 0",
            .AutoSize = True,
            .Location = New Point(120, 15)
        }
        panelSuperior.Controls.Add(lblPuntaje)

        lblVidas = New Label With {
            .Text = "Vidas: 5",
            .AutoSize = True,
            .Location = New Point(230, 15)
        }
        panelSuperior.Controls.Add(lblVidas)

        lblGasolina = New Label With {
            .Text = $"Gasolina: {gasolinaInicial}",
            .AutoSize = True,
            .Location = New Point(340, 15)
        }
        panelSuperior.Controls.Add(lblGasolina)
    End Sub

    ' Configuración de los Timers
    Private Sub ConfigurarTimers()
        timerJuego = New Timer With {.Interval = 100} ' Cada 50ms
        AddHandler timerJuego.Tick, AddressOf ActualizarJuego

        timerBuque = New Timer With {.Interval = 50} ' Cada 50ms
        AddHandler timerBuque.Tick, AddressOf MoverBuque

        ' Iniciar Timers
        timerJuego.Start()
        timerBuque.Start()
    End Sub

    ' Método principal para actualizar el juego
    Private Sub ActualizarJuego(sender As Object, e As EventArgs)
        ' Incrementar el contador de ticks
        ticksContador += 1

        ' Actualizar tiempo cada 10 ticks (ajusta este valor para cambiar la velocidad del tiempo)
        If ticksContador >= 10 Then
            tiempo += 1
            lblTiempo.Text = $"Tiempo: {tiempo}"
            ticksContador = 0

            ' Reducir gasolina de la lancha uno por uno
            If lancha.Gasolina > 0 Then
                lancha.Gasolina -= 1 ' Reducir de uno en uno
                lblGasolina.Text = $"Gasolina: {lancha.Gasolina}"
            Else
                lblGasolina.Text = "Gasolina: 0 (Recarga necesaria)"
            End If
        End If

        ' Mover la lancha si tiene gasolina
        If lancha.Gasolina > 0 Then
            lancha.Mover()
        End If

        ' Generar tiburones aleatoriamente
        If tiburones.Count < nivel * 2 AndAlso random.Next(0, 20) = 0 Then
            Dim posicionInicial As New Point(random.Next(0, Me.ClientSize.Width - 50), random.Next(50, Me.ClientSize.Height - 50))
            Dim velocidadInicial As New Point(random.Next(-5, 6), random.Next(-5, 6))
            Dim tiburon As New Tiburon(posicionInicial, velocidadInicial, CType(PB_Tiburon.Image.Clone(), Image))
            tiburones.Add(tiburon)
            Me.Controls.Add(tiburon.Imagen)
        End If

        ' Mover tiburones y verificar colisiones
        For Each tiburon In tiburones
            tiburon.Mover()
            tiburon.Rebotar(New Rectangle(0, 50, Me.ClientSize.Width, Me.ClientSize.Height))
            If lancha.Imagen.Bounds.IntersectsWith(tiburon.Imagen.Bounds) Then
                lancha.Vidas -= 1
                lblVidas.Text = $"Vidas: {lancha.Vidas}"
                If lancha.Vidas <= 0 Then
                    MessageBox.Show("¡Juego terminado! La lancha ha sido destruida.")
                    timerJuego.Stop()
                    timerBuque.Stop()
                    Exit Sub
                End If
            End If
        Next

        ' Generar sobrevivientes aleatoriamente
        If sobrevivientes.Count < nivel * 3 AndAlso random.Next(0, 30) = 0 Then
            Dim posicionInicial As New Point(random.Next(0, Me.ClientSize.Width - 50), random.Next(50, Me.ClientSize.Height - 50))
            Dim velocidadInicial As New Point(random.Next(-5, 6), random.Next(-5, 6))
            Dim sobreviviente As New Sobreviviente(posicionInicial, velocidadInicial, CType(PB_Sobreviviente.Image.Clone(), Image))
            sobrevivientes.Add(sobreviviente)
            Me.Controls.Add(sobreviviente.Imagen)
        End If

        ' Mover sobrevivientes y verificar si son recogidos
        For Each sobreviviente In sobrevivientes.ToList()
            sobreviviente.Mover()
            sobreviviente.Rebotar(New Rectangle(0, 50, Me.ClientSize.Width, Me.ClientSize.Height))
            If lancha.RecogerSobreviviente(sobreviviente.Imagen) Then
                Me.Controls.Remove(sobreviviente.Imagen)
                sobrevivientes.Remove(sobreviviente)
            End If
        Next

        ' Verificar colisiones entre tiburones y sobrevivientes
        For Each tiburon In tiburones
            For Each sobreviviente In sobrevivientes.ToList()
                If tiburon.Imagen.Bounds.IntersectsWith(sobreviviente.Imagen.Bounds) Then
                    ' Eliminar el sobreviviente
                    Me.Controls.Remove(sobreviviente.Imagen)
                    sobrevivientes.Remove(sobreviviente)

                    ' Restar puntos al jugador
                    If lancha.Puntos >= 10 Then ' Evitar puntaje negativo
                        lancha.Puntos -= 10
                    Else
                        lancha.Puntos = 0
                    End If
                    lblPuntaje.Text = $"Puntaje: {lancha.Puntos}"
                End If
            Next
        Next

        ' Detectar colisión con el buque
        If buque.ColisionConLancha(lancha.Imagen) Then
            ' Entregar sobrevivientes sin penalización
            lancha.EntregarSobrevivientes()
            lblPuntaje.Text = $"Puntaje: {lancha.Puntos}"
            If lancha.IncrementarNivel() Then
                nivel += 1
                MessageBox.Show($"¡Nivel {nivel} alcanzado! Los tiburones y sobrevivientes se mueven más rápido.")
            End If
        End If
    End Sub

    ' Método para mover el buque
    Private Sub MoverBuque(sender As Object, e As EventArgs)
        buque.Mover()
    End Sub

    ' Método para capturar teclas y cambiar la dirección de la lancha
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If lancha.Gasolina > 0 Then
            lancha.CambiarDireccion(e.KeyCode)
        End If
    End Sub
End Class
