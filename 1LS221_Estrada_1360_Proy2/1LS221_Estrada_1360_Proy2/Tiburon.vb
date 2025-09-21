Public Class Tiburon
    ' Propiedades
    Public Property Imagen As PictureBox ' Representación visual del tiburón
    Public Property Velocidad As Point   ' Velocidad de movimiento (X, Y)

    ' Constructor
    Public Sub New(posicionInicial As Point, velocidadInicial As Point, imagenRecurso As Image)
        ' Inicializar el PictureBox
        Imagen = New PictureBox With {
            .Image = imagenRecurso,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Size = New Size(50, 50), ' Tamaño del tiburón
            .Location = posicionInicial ' Posición inicial
        }

        ' Configurar velocidad inicial
        Velocidad = velocidadInicial
    End Sub

    ' Método para mover el tiburón
    Public Sub Mover()
        Imagen.Left += Velocidad.X
        Imagen.Top += Velocidad.Y
    End Sub

    ' Método para manejar colisiones con los bordes del formulario
    Public Sub Rebotar(limites As Rectangle)
        ' Rebote en los bordes izquierdo y derecho
        If Imagen.Left < limites.Left OrElse Imagen.Right > limites.Right Then
            Velocidad = New Point(-Velocidad.X, Velocidad.Y) ' Invertir la dirección X
        End If

        ' Rebote en los bordes superior e inferior
        If Imagen.Top < limites.Top OrElse Imagen.Bottom > limites.Bottom Then
            Velocidad = New Point(Velocidad.X, -Velocidad.Y) ' Invertir la dirección Y
        End If
    End Sub

    ' Método para manejar colisiones con otros tiburones o la lancha
    Public Sub RebotarSiColisiona(otrosObjetos As List(Of PictureBox))
        For Each otro In otrosObjetos
            If Imagen.Bounds.IntersectsWith(otro.Bounds) Then
                ' Cambiar la dirección al colisionar
                Velocidad = New Point(-Velocidad.X, -Velocidad.Y)
                Exit For
            End If
        Next
    End Sub

    ' Método para comprobar si el tiburón "come" un sobreviviente
    Public Function ComerSobreviviente(sobreviviente As PictureBox) As Boolean
        If Imagen.Bounds.IntersectsWith(sobreviviente.Bounds) Then
            Return True ' El sobreviviente ha sido comido
        End If
        Return False
    End Function
End Class
