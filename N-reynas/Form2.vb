Public Class Form2


    'matriz de los tableros
    Public TableroViejo(8, 8) As String
    Public TableroNuevo(8, 8) As String

    Public Sub MostrarTablero(Dimension As Integer)

        Dim i As Integer = 1 'NUMERO Y NOMBRE DEL BOTON
        Dim Fila As Integer = 0 'LLEVA EL NUMERO DE FILA
        Dim boton As Button
        'VERIFICA QUE LA DIMENSION MAXIMA DEL TABLERO SEA MENOR O IGUAL 10

        Panel1.Controls.Clear()

        For c = 0 To Dimension - 1
            For f = 0 To Dimension - 1

                boton = New System.Windows.Forms.Button
                boton.Name = i
                boton.Text = ""
                boton.SetBounds(f * 70, c * 70, 70, 70)

                If Dimension Mod 2 <> 0 Then 'SI LA DIMENSION DEL TABLERO ES PAR
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

                AddHandler boton.Click, AddressOf eventoClick
                Panel1.Controls.Add(boton)

                i = i + 1
            Next
            Fila = Fila + 1
        Next


        Me.Panel1.Width = 70 * nudDim.Value
        Me.Panel1.Height = 70 * nudDim.Value

    End Sub

    Private Sub eventoClick(ByVal sender As Object, e As EventArgs)

        Dim BotonTablero As Button = DirectCast(sender, Button) 'Nos declaramos un  variable  tipo button



        'funcion para obtener la pocision al dar un clik
        Dim f As Integer = 0
        Dim c As Integer = 0
        Dim nombre As Integer
        nombre = BotonTablero.Name

        f = Math.Truncate(nombre / Me.nudDim.Value)

        If (nombre Mod Me.nudDim.Value) = 0 Then

            c = Me.nudDim.Value - 1
            f = f - 1
            ' MsgBox(c & " - " & f)
        Else
            c = nombre - ((f * Me.nudDim.Value) + 1)

            '  MsgBox(c & " - " & f)
        End If

        If IsNothing(BotonTablero.Image) Then 'Preguntamos si el Boton tiene imagen
            BotonTablero.Image = Me.ImageList1.Images(0) 'Se inserta imagen al Boton
            TableroNuevo(c, f) = "R"
        Else
            BotonTablero.Image = Nothing ' Quita la imagen del boton
            TableroNuevo(c, f) = "0"
        End If

        Me.MostrarTableros(Me.nudDim.Value)

        If Me.ReinaConflictoDiagPrincipal(Me.nudDim.Value) = True Or
            Me.ReinaConflictoFila(Me.nudDim.Value, f) = True Or
            Me.ReinaConflictoColumna(Me.nudDim.Value, c) = True Or
            Me.ReinaConflictoDiagSecundario(Me.nudDim.Value) Then
            MsgBox("Reinas en conflicto, ha perdido intente nuevamente.", MsgBoxStyle.Critical, "N-Reinas")
            Me.btnTablero.PerformClick()
            Me.LimpiarTablero()
        End If
        'ReinaConflictoDiag1(Me.nudDim.Value)
        'ReinaConflictoColumna(Me.nudDim.Value, c)
        'ReinaConflictoFila(Me.nudDim.Value, f)
    End Sub

    Private Sub btnComprobar_Click(sender As Object, e As EventArgs)
        '    MsgBox(ReinasEnConflictos(Me.nudDim.Value, f, c))
    End Sub

    Function ReinaConflictoColumna(ByVal Dimension As Integer, ByVal Col As Integer) As Boolean


        'EVALUAR COLUMNA
        Dim Cont As Integer = 0
        ReinaConflictoColumna = False

        For f As Integer = 0 To Dimension - 1
            If TableroNuevo(Col, f) = "R" Then
                Cont = Cont + 1
            End If
        Next

        Dim Boton As Button
        If Cont > 1 Then
            ReinaConflictoColumna = True

            For f As Integer = 0 To Dimension - 1
                If TableroNuevo(Col, f) = "R" Then
                    Boton = Me.ObtenerBoton(Me.nudDim.Value, f, Col)
                    Boton.Image = Me.ImageList1.Images(1) 'Se inserta imagen al Boton
                End If
            Next
        End If
    End Function

    Function ReinaConflictoFila(ByVal Dimension As Integer, ByVal Fila As Integer) As Boolean


        'EVALUAR COLUMNA
        Dim Cont As Integer = 0
        ReinaConflictoFila = False

        For c As Integer = 0 To Dimension - 1
            If TableroNuevo(c, Fila) = "R" Then
                Cont = Cont + 1
            End If
        Next

        Dim Boton As Button
        If Cont > 1 Then
            ReinaConflictoFila = True

            For c As Integer = 0 To Dimension - 1
                If TableroNuevo(c, Fila) = "R" Then
                    Boton = Me.ObtenerBoton(Me.nudDim.Value, Fila, c)
                    Boton.Image = Me.ImageList1.Images(1) 'Se inserta imagen al Boton
                End If
            Next
     
        End If

    End Function

    Function ReinaConflictoDiagPrincipal(ByVal Dimension As Integer) As Boolean

        Dim Boton As Button
        'EVALUAR DIAGONAL \ \ \
        Dim COL As Integer = 0
        Dim FIL As Integer = Me.nudDim.Value - 2

        Dim ContAbajo As Integer = 0
        Dim ContArriba As Integer = 0
        ReinaConflictoDiagPrincipal = False

        'EMPIEZA EN 2 MENOS QUE LA DIMENSION Y VA HACIA ATRAS
        For i As Integer = Me.nudDim.Value - 2 To 0 Step -1
            COL = 0
            FIL = i

            ContAbajo = 0
            ContArriba = 0

            While FIL <= Dimension - 1
                If TableroNuevo(COL, FIL) = "R" Then
                    ContAbajo = ContAbajo + 1
                End If

                If FIL <> COL Then
                    If TableroNuevo(FIL, COL) = "R" Then
                        ContArriba = ContArriba + 1
                    End If
                End If

                COL = COL + 1
                FIL = FIL + 1
            End While


            If ContAbajo > 1 Or ContArriba > 1 Then

                ReinaConflictoDiagPrincipal = True

                COL = 0
                FIL = i

                While FIL <= Dimension - 1
                    If ContAbajo > 1 Then

                        ReinaConflictoDiagPrincipal = True

                        If TableroNuevo(COL, FIL) = "R" Then
                            Boton = Me.ObtenerBoton(Me.nudDim.Value, FIL, COL)
                            Boton.Image = Me.ImageList1.Images(1) 'Se inserta imagen al Boton
                        End If

                    Else
                        If TableroNuevo(COL, FIL) = "R" Then
                            Boton = Me.ObtenerBoton(Me.nudDim.Value, FIL, COL)
                            Boton.Image = Me.ImageList1.Images(0) 'Se inserta imagen al Boton
                        End If
                    End If

                    If ContArriba > 1 Then
                        If FIL <> COL Then
                            If TableroNuevo(FIL, COL) = "R" Then
                                Boton = Me.ObtenerBoton(Me.nudDim.Value, COL, FIL)
                                Boton.Image = Me.ImageList1.Images(1) 'Se inserta imagen al Boton
                            End If
                        End If
                    Else
                        If FIL <> COL Then
                            If TableroNuevo(FIL, COL) = "R" Then
                                Boton = Me.ObtenerBoton(Me.nudDim.Value, COL, FIL)
                                Boton.Image = Me.ImageList1.Images(0) 'Se inserta imagen al Boton
                            End If
                        End If

                    End If

                    COL = COL + 1
                    FIL = FIL + 1

                End While
            Else
                COL = 0
                FIL = i

                While FIL <= Dimension - 1

                    If TableroNuevo(COL, FIL) = "R" Then
                        Boton = Me.ObtenerBoton(Me.nudDim.Value, FIL, COL)
                        Boton.Image = Me.ImageList1.Images(0) 'Se inserta imagen al Boton
                    End If

                    If FIL <> COL Then
                        If TableroNuevo(FIL, COL) = "R" Then
                            Boton = Me.ObtenerBoton(Me.nudDim.Value, COL, FIL)
                            Boton.Image = Me.ImageList1.Images(0) 'Se inserta imagen al Boton
                        End If
                    End If

                    COL = COL + 1
                    FIL = FIL + 1
                End While
            End If

        Next

    End Function

    Function ReinaConflictoDiagSecundario(ByVal Dimension As Integer) As Boolean
        ReinaConflictoDiagSecundario = False
        Dim Boton As Button
        'EVALUAR DIAGONAL \\\

        Dim COL As Integer
        Dim FIL As Integer
        'Dim cantidad As Integer = Me.nudDim.Value * 2 - 3
        Dim Cont As Integer
        'EMPIEZA EN 2 MENOS QUE LA DIMENSION Y VA HACIA ATRAS
        For i As Integer = 1 To Me.nudDim.Value - 1
            Cont = 0
            COL = i
            FIL = 0

            While COL >= 0
                If TableroNuevo(COL, FIL) = "R" Then
                    Cont = Cont + 1
                End If

                COL = COL - 1
                FIL = FIL + 1

            End While
            If Cont > 1 Then

                ReinaConflictoDiagSecundario = True
                COL = i
                FIL = 0

                While COL >= 0

                    Boton = Me.ObtenerBoton(Me.nudDim.Value, FIL, COL)
                    If TableroNuevo(COL, FIL) = "R" Then
                        Boton.Image = Me.ImageList1.Images(1) 'Se inserta imagen al Boton
                    End If

                    COL = COL - 1
                    FIL = FIL + 1

                End While
            End If
        Next

        For i As Integer = 1 To Me.nudDim.Value - 1
            Cont = 0
            COL = Dimension - 1
            FIL = i

            While FIL <= Dimension - 1

                If TableroNuevo(COL, FIL) = "R" Then

                    Cont = Cont + 1
                End If
                COL = COL - 1
                FIL = FIL + 1

            End While
            If Cont > 1 Then
                ReinaConflictoDiagSecundario = True
                COL = Dimension - 1
                FIL = i
                While FIL <= Dimension - 1

                    Boton = Me.ObtenerBoton(Me.nudDim.Value, FIL, COL)
                    If TableroNuevo(COL, FIL) = "R" Then
                        Boton.Image = Me.ImageList1.Images(1) 'Se inserta imagen al Boton
                    End If
                    COL = COL - 1
                    FIL = FIL + 1

                End While
            End If

        Next


    End Function

    Function ObtenerBoton(dimension As Integer, fila As Integer, columna As Integer) As Button
        Dim numeroBoton As Integer

        numeroBoton = (fila * dimension) + columna

        numeroBoton = numeroBoton + 1

        Dim NUEVOBOTON As Button

        For Each NUEVOBOTON In Me.Panel1.Controls
            If NUEVOBOTON.Name = numeroBoton Then
                Return NUEVOBOTON
                Exit For
            End If
        Next
      
    End Function

    Function ReinasEnConflicto2(ByVal fila As Integer, ByVal columna As Integer) As Boolean

        Dim f As Integer = fila
        Dim c As Integer = 0


        'EVALUAR COLUMNAS


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
        While (f < Me.nudDim.Value And c >= 0)
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

    Private Sub btnTablero_Click(sender As Object, e As EventArgs) Handles btnTablero.Click
        'CARGA LOS BOTONES SEGUN EL TAMAÑO DEL TABLERO
        MostrarTablero(Me.nudDim.Value)
        'LIMPIA TODO EL TABLERO
        LimpiarTablero()


    End Sub

    Sub LimpiarTablero()

        Dim f, c As Integer

        f = 0

        While f < Me.nudDim.Value

            While c < Me.nudDim.Value
                TableroNuevo(f, c) = "0"
                c += 1
            End While

            c = 0
            f += 1

        End While

        Me.TextBox1.Clear()

    End Sub

    Private Sub nudDim_GotFocus(sender As Object, e As EventArgs) Handles nudDim.GotFocus
        Me.nudDim.BackColor = Color.Cyan
    End Sub

    Private Sub nudDim_LostFocus(sender As Object, e As EventArgs) Handles nudDim.LostFocus
        Me.nudDim.BackColor = Color.White

    End Sub

    Private Sub btnResolver_Click(sender As Object, e As EventArgs) Handles btnResolver.Click

        'LIMPIA TABLERO NUEVO
        Me.LimpiarTablero()

        'QUITA IMAGENES DE LOS BOTONES
        Me.QuitarImagenes()

        Randomize()
        ' Generate random value between 5 and 1
        Dim fila As Integer = CInt(Int((Me.nudDim.Value * Rnd()) + 0))

        Randomize()
        ' Generate random value between 5 and 1
        Dim columna As Integer = CInt(Int((Me.nudDim.Value * Rnd()) + 0))

        '' ASIGNAMOS UNA REINA ALEATORIA
        TableroNuevo(fila, columna) = "R"

        Me.Resolver()

        Me.MostrarTableros(Me.nudDim.Value)

        While PreguntarIgual(Me.nudDim.Value) = True Or CantidadCorrecta(Me.nudDim.Value) = False

            'LIMPIAMOS TABLER NUEVO
            Me.LimpiarTablero()

            Randomize()
            fila = CInt(Int((Me.nudDim.Value * Rnd()) + 0))
            Randomize()
            columna = CInt(Int((Me.nudDim.Value * Rnd()) + 0))

            '' ASIGNAMOS UNA REINA ALEATORIA
            TableroNuevo(fila, columna) = "R"

            Me.Resolver()
            Me.MostrarTableros(Me.nudDim.Value)


        End While


        For i As Integer = 0 To Me.nudDim.Value - 1
            For j As Integer = 0 To Me.nudDim.Value - 1
                TableroViejo(i, j) = TableroNuevo(i, j)
            Next
        Next

        Me.mostrarResultado(Me.nudDim.Value)
    End Sub

    Sub QuitarImagenes()
        For Each NUEVOBOTON In Me.Panel1.Controls
            NUEVOBOTON.Image = Nothing
        Next
    End Sub

    Sub Resolver()
        Dim exito As Boolean = False
        backtrackingReinas(0, exito)
    End Sub

    Function backtrackingReinas(ByVal c As Integer, ByRef exito As Boolean) As Boolean

        Dim f As Integer = 0

        While f < Me.nudDim.Value And exito = False

            If ReinasEnConflicto(f, c) = False Then

                TableroNuevo(f, c) = "R"  ' 'Anotar

                If c = Me.nudDim.Value - 1 Then
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
        While (f < Me.nudDim.Value And c >= 0)
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

    Sub MostrarTableros(Dimension As Integer)
        Dim cadena As String = ""
        Dim Cadena2 As String = "{"

        For f As Integer = 0 To Dimension - 1
            For c As Integer = 0 To Dimension - 1
                If TableroNuevo(f, c) = "R" Then
                    cadena = cadena & "(" & f & "," & c & ")" & vbNewLine
                    Cadena2 = Cadena2 & c & ","
                End If
            Next
        Next

        If Cadena2.Length > 1 Then
            Cadena2 = Mid(Cadena2, 1, Cadena2.Length - 1) & "}"
        Else
            Cadena2 = "{}"
        End If
        'Cadena2 = Mid(Cadena2, 1, Cadena2.Length - 1) & "}"

        Me.TextBox1.Text = cadena & vbNewLine & Cadena2
    End Sub

    Function PreguntarIgual(Dimension) As Boolean
        PreguntarIgual = True

        For i As Integer = 0 To Dimension - 1
            For j As Integer = 0 To Dimension - 1
                If TableroNuevo(i, j) <> TableroViejo(i, j) Then
                    Return False
                End If
            Next
        Next
    End Function

    Function CantidadCorrecta(Dimension As Integer) As Boolean
        Dim contador As Integer = 0

        For i As Integer = 0 To Dimension - 1
            For j As Integer = 0 To Dimension - 1
                If TableroNuevo(i, j) = "R" Then
                    contador = contador + 1
                End If
            Next
        Next

        If contador <> Dimension Then
            Return False
        Else
            Return True
        End If
    End Function

    Sub mostrarResultado(Dimension As Integer)
        ''Esta función no se muestra pues depende de
        ''si deseas mostrar el resultado en consola o a través de un formulario.

        Dim numeroBoton As Integer

        For i As Integer = 0 To Dimension - 1
            For j As Integer = 0 To Dimension - 1
                If TableroNuevo(i, j) = "R" Then

                    numeroBoton = (i * Dimension) + j
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

    End Sub

   
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MostrarTablero(Me.nudDim.Value)
        'LIMPIA TODO EL TABLERO
        LimpiarTablero()

    End Sub

    Private Sub nudDim_ValueChanged(sender As Object, e As EventArgs) Handles nudDim.ValueChanged
        'CAMBIA EL TAMAÑO DE LA VARIABLE TABLERO
        ReDim TableroViejo(Me.nudDim.Value - 1, Me.nudDim.Value - 1)
        ReDim TableroNuevo(Me.nudDim.Value - 1, Me.nudDim.Value - 1)

        'CARGA LOS BOTONES SEGUN EL TAMAÑO DEL TABLERO
        MostrarTablero(Me.nudDim.Value)

        'LIMPIA TODO EL TABLERO
        LimpiarTablero()
    End Sub

    Private Sub SALIRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SALIRToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub AYUDAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AYUDAToolStripMenuItem.Click
        Dim frm As New INFORMACION
        frm.ShowDialog()
    End Sub
End Class