Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System
Imports System.Collections
Public Class Form1


    'vatiables globales

    Friend WithEvents boton As System.Windows.Forms.Button

    '  Dim BTN(10, 10) As String

    Public TableroViejo(8, 8) As String
    Public TableroNuevo(8, 8) As String


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    

    End Sub
    Sub Resolver()

        Dim exito As Boolean = False

        backtrackingReinas(0, exito)


    End Sub

    Sub LimpiarTablero()

        Dim f, c As Integer

        f = 0

        While f < nupDimension.Value

            While c < nupDimension.Value
                TableroNuevo(f, c) = "0"
                c += 1
            End While

            c = 0
            f += 1

        End While

    End Sub
    Sub QuitarImagenes()
        For Each NUEVOBOTON In Me.Panel1.Controls
            NUEVOBOTON.Image = Nothing
        Next
    End Sub
    Sub mostrarResultado()
        ''Esta función no se muestra pues depende de
        ''si deseas mostrar el resultado en consola o a través de un formulario.

        Dim numeroBoton As Integer

        For i As Integer = 0 To Me.nupDimension.Value - 1
            For j As Integer = 0 To Me.nupDimension.Value - 1
                If TableroNuevo(i, j) = "R" Then

                    numeroBoton = (i * Me.nupDimension.Value) + j

                    '     MsgBox(j & " -  " & i)


                    numeroBoton = numeroBoton + 1



                    Dim NUEVOBOTON As Button

                    For Each NUEVOBOTON In Me.Panel1.Controls
                        If NUEVOBOTON.Name = numeroBoton Then
                            NUEVOBOTON.Image = Me.ImageList1.Images(0)
                            Exit For
                        End If

                    Next
                End If
            Next
        Next

        'Dim CADENA As String = ""

        'Dim numeroBoton As Integer = 1

        'For i As Integer = 0 To Me.nupDimension.Value - 1
        '    For j As Integer = 0 To Me.nupDimension.Value - 1


        '        CADENA = CADENA + tablero(i, j)
        '    Next

        '    CADENA = CADENA + vbNewLine
        'Next

        'MsgBox(CADENA)

    End Sub

    Function backtrackingReinas(ByVal c As Integer, ByRef exito As Boolean) As Boolean

        Dim f As Integer = 0

        While f < nupDimension.Value And exito = False

            If ReinasEnConflicto(f, c) = False Then

                TableroNuevo(f, c) = "R"  ' 'Anotar

                If c = nupDimension.Value - 1 Then
                    exito = True
                Else
                    backtrackingReinas(c + 1, exito)
                End If

            End If

            If exito = False Then

                TableroNuevo(f, c) = "0"  ''Desanotar

            End If

            f += 1

        End While

        Return f

    End Function

    Function ReinasEnConflicto(ByVal fila As Integer, ByVal columna As Integer) As Boolean

        Dim f As Integer = fila
        Dim c As Integer = 0

        ''EVALUAR FILAS
        While c < columna
    
            If TableroNuevo(f, c) = "R" Then
                Return True ''conflicto
            Else
                c += 1
            End If
            ''
        End While
        ''
        ''EVALUAR DIAGONAL SUPERIOR
        f = fila
        c = columna
        ''
        While (f >= 0 And c >= 0)
            ''
            If TableroNuevo(f, c) = "R" Then
                Return True ''conflicto
            Else
                f -= 1
                c -= 1
            End If
            ''
        End While
        ''
        'EVALUAR DIAGONAL INFERIOR
        f = fila
        c = columna
        '
        While (f < Me.nupDimension.Value And c >= 0)
            ''
            If TableroNuevo(f, c) = "R" Then
                Return True ''conflicto
            Else
                f += 1
                c -= 1
            End If
            ''
        End While

        Return False

    End Function


#Region " funciones del Juego"
    'Function backtracking(ByVal c As Integer, ByRef exito As Boolean) As Boolean
    '    ''
    '    Dim f As Integer = 0
    '    ''
    '    While f < nupDimension.Value And exito = False
    '        ''
    '        If ReinasEnConflicto(f, c) = False Then
    '            ''
    '            tablero(f, c) = "R"  ' 'Anotar
    '            ''
    '            If c = nupDimension.Value - 1 Then
    '                exito = True
    '            Else
    '                backtrackingReinas(c + 1, exito)
    '            End If
    '            ''
    '        End If
    '        ''
    '        If exito = False Then
    '            ''
    '            tablero(f, c) = "0"  ''Desanotar
    '            ''
    '        End If
    '        ''
    '        f += 1
    '        ''
    '    End While
    '    Return f
    'End Function
    'Sub Mostrartablero()
    '    Dim i As Integer = 1
    '    Dim j As Integer = 0
    '    If DIMENCION.Value < 10 Then
    '        Panel1.Controls.Clear()
    '        For c = 0 To DIMENCION.Value - 1
    '            For f = 0 To DIMENCION.Value - 1
    '                If i = 25 Then
    '                    MsgBox("ok")
    '                End If
    '                boton = New System.Windows.Forms.Button
    '                boton.Left = 75
    '                boton.Name = i
    '                boton.Text = i
    '                boton.SetBounds(f * 70, c * 70, 70, 70)


    '                If DIMENCION.Value Mod 2 <> 0 Then 'pregunta si la dimencion de la matriz es distinto
    '                    If i Mod 2 = 0 Then  'pregunta si el contador i es impar
    '                        boton.BackColor = (Color.Black)
    '                    Else
    '                        boton.BackColor = (Color.White)
    '                    End If
    '                Else
    '                    If j Mod 2 = 0 Then 'pregunta si el contador j es par
    '                        If i Mod 2 = 0 Then
    '                            boton.BackColor = (Color.Black)
    '                        Else
    '                            boton.BackColor = (Color.White)
    '                        End If

    '                    Else
    '                        If i Mod 2 <> 0 Then
    '                            boton.BackColor = (Color.Black)
    '                        Else
    '                            boton.BackColor = (Color.White)
    '                        End If
    '                    End If

    '                End If
    '                i = i + 1

    '                AddHandler boton.Click, AddressOf eventoClick
    '                Panel1.Controls.Add(boton)

    '            Next
    '            j = j + 1
    '        Next

    '    End If
    'End Sub

    Sub Mostrartablero()

        Dim i As Integer = 1 'NUMERO Y NOMBRE DEL BOTON

        Dim Fila As Integer = 0 'LLEVA EL NUMERO DE FILA

        'VERIFICA QUE LA DIMENSION MAXIMA DEL TABLERO SEA MENOR O IGUAL 10
        If nupDimension.Value <= 10 Then
            Panel1.Controls.Clear()

            For c = 0 To nupDimension.Value - 1
                For f = 0 To nupDimension.Value - 1


                    boton = New System.Windows.Forms.Button
                    'boton.Left = 75
                    boton.Name = i
                    boton.Text = i
                    boton.SetBounds(f * 70, c * 70, 70, 70)

                    If nupDimension.Value Mod 2 <> 0 Then 'SI LA DIMENSION DEL TABLERO ES PAR
                        If i Mod 2 = 0 Then  'PREGUNTA SI I CONTADOR ES PAR
                            boton.BackColor = (Color.Black)
                            boton.ForeColor = Color.White

                        Else
                            boton.BackColor = (Color.White)
                            boton.ForeColor = Color.Black

                        End If

                    Else 'SI LA DIMENSION DEL TABLERO ES IMPAR

                        If Fila Mod 2 = 0 Then 'SI LA FILA ES PAR PINTA LOS PARES BLANCOS
                            If i Mod 2 = 0 Then
                                boton.BackColor = (Color.Black)
                                boton.ForeColor = Color.White

                            Else
                                boton.BackColor = (Color.White)
                                boton.ForeColor = Color.Black

                            End If
                        Else  'SI LA FILA ES IMPAR PINTA LOS PARES NEGROS
                            If i Mod 2 <> 0 Then
                                boton.BackColor = (Color.Black)
                                boton.ForeColor = Color.White

                            Else
                                boton.BackColor = (Color.White)
                                boton.ForeColor = Color.Black

                            End If
                        End If
                    End If

                    MsgBox(i)
                    AddHandler boton.Click, AddressOf eventoClick
                    Panel1.Controls.Add(boton)

                    i = i + 1
                Next
                Fila = Fila + 1
            Next


        End If
    End Sub
    Sub ColorearBotones(Numero As Integer)

        

        If nupDimension.Value Mod 2 <> 0 Then 'SI LA DIMENSION DEL TABLERO ES PAR
            If Numero Mod 2 = 0 Then  'PREGUNTA SI I CONTADOR ES PAR
                boton.BackColor = (Color.Black)
                boton.ForeColor = Color.White

            Else
                boton.BackColor = (Color.White)
                boton.ForeColor = Color.Black

            End If

        Else 'SI LA DIMENSION DEL TABLERO ES IMPAR
            Dim fila As Integer


            If Fila Mod 2 = 0 Then 'SI LA FILA ES PAR PINTA LOS PARES BLANCOS
                If Numero Mod 2 = 0 Then
                    boton.BackColor = (Color.Black)
                    boton.ForeColor = Color.White

                Else
                    boton.BackColor = (Color.White)
                    boton.ForeColor = Color.Black

                End If
            Else  'SI LA FILA ES IMPAR PINTA LOS PARES NEGROS
                If Numero Mod 2 <> 0 Then
                    boton.BackColor = (Color.Black)
                    boton.ForeColor = Color.White

                Else
                    boton.BackColor = (Color.White)
                    boton.ForeColor = Color.Black

                End If
            End If
        End If


    End Sub
    Private Sub eventoClick(ByVal sender As Object, e As EventArgs) Handles boton.Click

        Dim BotonTablero As Button = DirectCast(sender, Button)

        If IsNothing(BotonTablero.Image) Then
            BotonTablero.Image = Me.ImageList1.Images(0)
        Else
            BotonTablero.Image = Nothing
        End If

        Dim i, j As Integer

        Dim Num As Integer

        Num = CInt(BotonTablero.Name)

        j = CInt(Num / Me.nupDimension.Value)

        i = Math.Abs(Num - (j * Me.nupDimension.Value + 1))

        MsgBox("(" & i & "," & j & ")")
  

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BTN_INICIAR.Click

        'CAMBIA EL TAMAÑO DE LA VARIABLE TABLERO
        ReDim tableroViejo(Me.nupDimension.Value - 1, Me.nupDimension.Value - 1)
        ReDim TableroNuevo(Me.nupDimension.Value - 1, Me.nupDimension.Value - 1)

        'CARGA LOS BOTONES SEGUN EL TAMAÑO DEL TABLERO
        Mostrartablero()

        'LIMPIA TODO EL TABLERO
        LimpiarTablero()


        ' Resolver()

    End Sub

#End Region

    Sub MostrarTableros()
        Dim cadena As String = ""
        For i As Integer = 0 To Me.nupDimension.Value - 1
            For j As Integer = 0 To Me.nupDimension.Value - 1
                If TableroNuevo(i, j) = "R" Then
                    cadena = cadena & "(" & j & "," & i & ")" & vbNewLine
                End If
            Next
        Next

        Me.TextBox1.Text = cadena

        'cadena = ""
        'For i As Integer = 0 To Me.nupDimension.Value - 1
        '    For j As Integer = 0 To Me.nupDimension.Value - 1
        '        cadena = cadena + tableroViejo(i, j)
        '    Next
        '    cadena = cadena + vbNewLine
        'Next

        'Me.TextBox2.Text = cadena


    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        'LIMPIA TABLERO NUEVO
        Me.LimpiarTablero()

        'QUITA IMAGENES DE LOS BOTONES
        Me.QuitarImagenes()

        Randomize()
        ' Generate random value between 5 and 1
        Dim fila As Integer = CInt(Int((Me.nupDimension.Value * Rnd()) + 0))

        Randomize()
        ' Generate random value between 5 and 1
        Dim columna As Integer = CInt(Int((Me.nupDimension.Value * Rnd()) + 0))

        '' ASIGNAMOS UNA REINA ALEATORIA
        TableroNuevo(fila, columna) = "R"

        Me.Resolver()

        MostrarTableros()

        While PreguntarIgual() = True Or CantidadCorrecta() = False

            'LIMPIAMOS TABLER NUEVO
            Me.LimpiarTablero()

            Randomize()
            fila = CInt(Int((Me.nupDimension.Value * Rnd()) + 0))
            Randomize()
            columna = CInt(Int((Me.nupDimension.Value * Rnd()) + 0))

            '' ASIGNAMOS UNA REINA ALEATORIA
            TableroNuevo(fila, columna) = "R"

            Me.Resolver()
            MostrarTableros()

        End While


        For i As Integer = 0 To Me.nupDimension.Value - 1
            For j As Integer = 0 To Me.nupDimension.Value - 1
                tableroViejo(i, j) = TableroNuevo(i, j)
            Next
        Next

        mostrarResultado()

    End Sub

    Function PreguntarIgual() As Boolean
        PreguntarIgual = True

        For i As Integer = 0 To Me.nupDimension.Value - 1
            For j As Integer = 0 To Me.nupDimension.Value - 1
                If TableroNuevo(i, j) <> tableroViejo(i, j) Then
                    Return False
                End If
            Next
        Next
    End Function


    Function CantidadCorrecta() As Boolean
        Dim contador As Integer = 0

        For i As Integer = 0 To Me.nupDimension.Value - 1
            For j As Integer = 0 To Me.nupDimension.Value - 1
                If TableroNuevo(i, j) = "R" Then
                    contador = contador + 1
                End If
            Next
        Next

        If contador <> Me.nupDimension.Value Then
            Return False
        Else
            Return True
        End If
    End Function

   
End Class
