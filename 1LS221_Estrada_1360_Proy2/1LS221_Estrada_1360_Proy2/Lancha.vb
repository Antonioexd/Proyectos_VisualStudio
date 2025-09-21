Public Class Lancha
    ' Propiedades
    Public Property Imagen As PictureBox ' Representación visual de la lancha
    Public Property Velocidad As Point   ' Velocidad de movimiento (X, Y)
    Public Property Gasolina As Integer  ' Tiempo restante de gasolina (en segundos)
    Public Property SobrevivientesEnLancha As Integer ' Contador de sobrevivientes en la lancha
    Public Property Puntos As Integer    ' Puntos acumulados
    Public Property Vidas As Integer     ' Vidas restantes
    Public Property Capacidad As Integer

    ' Constantes
    Private Const MAX_VELOCIDAD As Integer = 50 ' Velocidad máxima
    Private Const GASOLINA_INICIAL As Integer = 60 ' Gasolina inicial
    Private Const MAX_SOBREVIVIENTES As Integer = 3 ' Capacidad máxima de la lancha

    ' Constructor
    Public Sub New(posicionInicial As Point, imagenRecurso As Image)
        ' Inicializar el PictureBox
        Imagen = New PictureBox With {
            .Image = imagenRecurso,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Size = New Size(60, 60),
            .Location = posicionInicial
        }

        ' Inicializar propiedades
        Velocidad = New Point(0, 0)
        Gasolina = GASOLINA_INICIAL
        SobrevivientesEnLancha = 0
        Puntos = 0
        Vidas = 5
    End Sub

    ' Método para mover la lancha
    Public Sub Mover()
        ' Si no tiene gasolina, no puede moverse
        If Gasolina > 0 Then
            Imagen.Left += Velocidad.X
            Imagen.Top += Velocidad.Y
            Gasolina -= 1 ' Reducir gasolina por movimiento
        End If
    End Sub

    ' Método para cambiar la dirección de la lancha
    Public Sub CambiarDireccion(tecla As Keys)
        ' Si no tiene gasolina, no puede cambiar de dirección
        If Gasolina <= 0 Then Return

        Select Case tecla
            Case Keys.Up
                Velocidad = New Point(Velocidad.X, Math.Max(Velocidad.Y - 5, -MAX_VELOCIDAD))
            Case Keys.Down
                Velocidad = New Point(Velocidad.X, Math.Min(Velocidad.Y + 5, MAX_VELOCIDAD))
            Case Keys.Left
                Velocidad = New Point(Math.Max(Velocidad.X - 5, -MAX_VELOCIDAD), Velocidad.Y)
            Case Keys.Right
                Velocidad = New Point(Math.Min(Velocidad.X + 5, MAX_VELOCIDAD), Velocidad.Y)
        End Select
    End Sub

    ' Método para recoger sobrevivientes
    Public Function RecogerSobreviviente(sobreviviente As PictureBox) As Boolean
        If Imagen.Bounds.IntersectsWith(sobreviviente.Bounds) Then
            If SobrevivientesEnLancha < MAX_SOBREVIVIENTES Then
                SobrevivientesEnLancha += 1
                Return True ' Sobreviviente recogido
            End If
        End If
        Return False
    End Function

    ' Método para entregar sobrevivientes al buque
    Public Function EntregarSobrevivientes() As Integer
        Dim puntosGanados As Integer = SobrevivientesEnLancha * 10
        Puntos += puntosGanados
        SobrevivientesEnLancha = 0
        RecargarGasolina() ' Recargar gasolina al entregar sobrevivientes
        Return puntosGanados
    End Function

    ' Método para recargar gasolina
    Public Sub RecargarGasolina()
        Gasolina = GASOLINA_INICIAL
    End Sub

    ' Método para manejar colisiones con tiburones
    Public Sub ColisionConTiburon(tiburon As PictureBox)
        If Imagen.Bounds.IntersectsWith(tiburon.Bounds) Then
            Vidas -= 1
            If Vidas <= 0 Then
                ' Lógica de fin del juego
                Throw New InvalidOperationException("La lancha ha sido destruida.")
            End If
        End If
    End Sub

    ' Método para verificar colisión con el buque a velocidad peligrosa
    Public Function ColisionConBuque(buque As PictureBox) As Boolean
        If Imagen.Bounds.IntersectsWith(buque.Bounds) AndAlso
           (Math.Abs(Velocidad.X) > 10 OrElse Math.Abs(Velocidad.Y) > 10) Then
            Vidas -= 1
            If Vidas <= 0 Then
                ' Lógica de fin del juego
                Throw New InvalidOperationException("La lancha se estrelló contra el buque.")
            End If
            Return True
        End If
        Return False
    End Function

    ' Método para incrementar el nivel según los puntos
    Public Function IncrementarNivel() As Boolean
        If Puntos >= 100 Then
            Puntos -= 100 ' Restar los puntos necesarios para el nivel
            Return True ' Nivel incrementado
        End If
        Return False
    End Function
End Class
