Public Class Buque
    ' Propiedades
    Public Property Imagen As PictureBox ' Representación visual del buque
    Public Property Velocidad As Integer ' Velocidad de movimiento en el eje X
    Private ReadOnly Limites As Rectangle ' Límites del formulario

    ' Constructor
    Public Sub New(posicionInicial As Point, velocidadInicial As Integer, imagenRecurso As Image, limitesFormulario As Rectangle)
        ' Inicializar el PictureBox
        Imagen = New PictureBox With {
            .Image = imagenRecurso,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Size = New Size(100, 40), ' Tamaño del buque
            .Location = posicionInicial ' Posición inicial
        }

        ' Configurar velocidad y límites
        Velocidad = velocidadInicial
        Limites = limitesFormulario
    End Sub

    ' Método para mover el buque
    Public Sub Mover()
        ' Actualizar posición
        Imagen.Left += Velocidad

        ' Si el buque desaparece por un lado, reaparece por el otro
        If Imagen.Right < Limites.Left Then
            Imagen.Left = Limites.Right
        ElseIf Imagen.Left > Limites.Right Then
            Imagen.Left = Limites.Left - Imagen.Width
        End If
    End Sub

    ' Método para detectar colisión con la lancha
    Public Function ColisionConLancha(lancha As PictureBox) As Boolean
        Return Imagen.Bounds.IntersectsWith(lancha.Bounds)
    End Function
End Class
