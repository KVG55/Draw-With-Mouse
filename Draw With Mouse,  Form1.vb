'©Виталий Караваев.


Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Globalization
Imports System.IO

Public Class Form1


#Region "Объявления и декларации"

    ''' <summary>
    ''' объявления и декларации
    ''' </summary>
    ''' <param name="bVk"></param>
    ''' <param name="bScan"></param>
    ''' <param name="dwFlags"></param>
    ''' <param name="dwExtraInfo"></param>
    ''' 
    Private Declare Sub keybd_event Lib "user32.dll" _
        (bVk As Byte, bScan As Byte,
dwFlags As Integer, dwExtraInfo As Integer)

    Private AllPoints() As Point
    Private PointLim As Integer
    Private bDrawing As Boolean
    Dim Color1, penColor As Color
    Private OpTy As Double = 0.1


    ' Радиус окружностей для меток; вращение изображения в ОК 
    Private Const CORNER_RADIUS As Integer = 7
    Private BmSource As Bitmap
    Private BmDest As Bitmap
    Private Corners As Point()
    Private DragCorner As Long


    ''' <summary>
    ''' Переменные для опции вывода пути к файлу
    ''' </summary>
    Private ReadOnly fileName As String

    Private m_FileLoaded As Boolean = False


    ''' <summary>
    ''' логические переменные для различных обращений к мыши
    ''' </summary>
    ''' 
    Private ContrlMouse, ContrlMouse1, ContrlMouse2, pt, sel, B As Boolean




    Private ContrlMouse10 As Boolean

    Private ReadOnly ris1 As Boolean = False ' - не рисовать
    Private ReadOnly ris10 As Boolean = False

    ''' <summary>
    ''' логическая переменная для схемы кода поиска и автодополнения
    ''' </summary>
    Private ContrlKey As Boolean = False



    Dim demoBrush As Brush

    ReadOnly image1 As Bitmap

    Private currentImage As Image = Nothing      'текущего изображения
    Private originalImage As Image = Nothing     'исходного изображения


    Private ReadOnly TextBox3 As New TextBox()


    Private ReadOnly formGraphics As Graphics = CreateGraphics()



    ''' <summary>
    ''' константа для свертывания элемента управления
    ''' </summary>
    Const gDirection As Integer = 10
    ''' <summary>
    ''' объявление и декларация для класа выделения области
    ''' </summary>
    Private ReadOnly SelectionArea As StretchableL = New StretchableL(Me)


    ''' переменная формата расширения файла графического изображения
    Private ReadOnly jpg As Imaging.ImageFormat = Nothing


    ''' рандомная переменная
    Private ReadOnly m_Random As New Random







#End Region

#Region "Загрузка формы"


    ''' <summary>
    ''' загрузка формы
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ToolStripTextBox6.Text = "1"


            TextBox1.Text() = "1"

            TextBox2.Text() = "1"

            TextBox4.Text() = "0"

            TextBox5.Text() = "0"

            BackColor = Color.White

            ToolStripComboBox5.SelectedItem = Color.Black

            ToolStripComboBox6.SelectedItem = "Times New Roman"


            ' присвоение значения выполнения переменным и константам вращения картинки
            BmSource = New Bitmap(PictureBox1.Image)
            BmDest = New Bitmap((BmSource.Width), (BmSource.Height))
            Corners = New Point() {New Point(0, 0), New Point(BmSource.Width, 0), New Point(0, BmSource.Height)}

            DragCorner = -1

            ' Выводим картинку. 
            WarpImage()


            'Оформление списка цветов в окно комбобокса циклом, для задания цвета 
            Dim i111 As Integer
            For i111 = 1 To 167
                'Если цвет не "Transparent":
                If i111 <> 27 Then _
            ToolStripComboBox3.Items.Add(Color.FromKnownColor((CType(i111, KnownColor))))
            Next
            'Сортировать записи в алфавитном порядке:
            ToolStripComboBox3.Sorted = True
            ' выбор пункта
            ToolStripComboBox3.SelectedItem = Color.Beige


            'Оформление списка цветов в окно комбобокса циклом, для задания цвета 
            Dim i1 As Integer
            For i1 = 1 To 167
                'Если цвет не "Transparent":
                If i1 <> 27 Then _
            ToolStripComboBox4.Items.Add(Color.FromKnownColor((CType(i1, KnownColor))))
            Next
            'Сортировать записи в алфавитном порядке:
            ToolStripComboBox4.Sorted = True

            ToolStripComboBox4.SelectedItem = Color.Olive



            Dim iA As Integer
            For iA = 1 To 167
                'Если цвет не "Transparent":
                If iA <> 27 Then _
            ToolStripComboBox5.Items.Add(Color.FromKnownColor((CType(iA, KnownColor))))
            Next
            'Сортировать записи в алфавитном порядке:
            ToolStripComboBox5.Sorted = True
            ' выбор пункта
            ToolStripComboBox5.SelectedItem = Color.Black


            'Оформление списка цветов в окно комбобокса циклом, для задания цвета 
            Dim i11 As Integer
            For i11 = 1 To 167
                'Если цвет не "Transparent":
                If i11 <> 27 Then _
            ComboBox1.Items.Add(Color.FromKnownColor((CType(i11, KnownColor))))
            Next
            'Сортировать записи в алфавитном порядке:
            ComboBox1.Sorted = True

            'Оформление списка цветов в окно комбобокса циклом, для задания цвета 
            Dim i11111 As Integer
            For i11111 = 1 To 167
                'Если цвет не "Transparent":
                If i11111 <> 27 Then _
            ComboBox2.Items.Add(Color.FromKnownColor((CType(i11111, KnownColor))))
            Next
            'Сортировать записи в алфавитном порядке:
            ComboBox2.Sorted = True


            'Оформление списка цветов в окно комбобокса циклом, для задания цвета 
            Dim i1111111 As Integer
            For i1111111 = 1 To 167
                'Если цвет не "Transparent":
                If i1111111 <> 27 Then _
            ComboBox3.Items.Add(Color.FromKnownColor((CType(i1111111, KnownColor))))
            Next
            'Сортировать записи в алфавитном порядке:
            ComboBox3.Sorted = True


            'Оформление списка цветов в окно комбобокса циклом, для задания цвета 
            Dim i111111 As Integer
            For i111111 = 1 To 167
                'Если цвет не "Transparent":
                If i111111 <> 27 Then _
            ComboBox4.Items.Add(Color.FromKnownColor((CType(i111111, KnownColor))))
            Next
            'Сортировать записи в алфавитном порядке:
            ComboBox4.Sorted = True



            RemoveHandler Me.Paint, AddressOf Form1_Paint


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
#End Region

#Region "Класс выделения области"
    Public Class StretchableL

        ' Переменные уровня класса
        Private BasePoint As Point
        Private ExtentPoint As Point
        Private CurrentState As StretchableLState
        Private ReadOnly BaseControl As Control

        ' Текущее состояние рисования
        Public Enum StretchableLState
            Inactive
            FirstTime
            Active
        End Enum

        Public Sub New(useControl As Control)
            ' Конструктор с одним параметром
            BaseControl = useControl

        End Sub
        Public ReadOnly Property Rectangle() As Rectangle
            Get
                'Возвращаем границы области, указанной растяжимой линией
                Dim result As Rectangle
                'Отсчет координат с левого верхнего угла вниз и направо.
                result.X = If(BasePoint.X < ExtentPoint.X, BasePoint.X, ExtentPoint.X)
                result.Y = If(BasePoint.Y < ExtentPoint.Y, BasePoint.Y, ExtentPoint.Y)
                result.Width = Math.Abs(ExtentPoint.X - BasePoint.X)
                result.Height = Math.Abs(ExtentPoint.Y - BasePoint.Y)
                Return result
            End Get
        End Property
        Public Sub Start(x As Integer, y As Integer)
            ' начинаем рисовать
            ' прорисовывать первое изображение линии, необходимо 
            'вызваать метод Stretch().
            BasePoint.X = x
            BasePoint.Y = y
            ExtentPoint.X = x
            ExtentPoint.Y = y
            Normalize(BasePoint)
            CurrentState = StretchableLState.FirstTime
        End Sub
        Public Sub Stretch(x As Integer, y As Integer)
            'Меняем размер линии
            Dim NewPoint As Point
            'Подготавливаем новую точку для растяжения
            NewPoint.X = x
            NewPoint.Y = y
            Normalize(NewPoint)
            Select Case CurrentState
                Case StretchableLState.Inactive
                    'Линия не активна
                    Return
                Case StretchableLState.FirstTime
                    'рисуем начальную линию
                    ExtentPoint = NewPoint
                    DrawTheRectangle()
                    CurrentState = StretchableLState.Active
                Case StretchableLState.Active
                    'удаляем предыдущую линию
                    'потом рисуем новую
                    DrawTheRectangle()
                    ExtentPoint = NewPoint
                    DrawTheRectangle()
            End Select
        End Sub
        Public Sub Finish()
            'прекращаем рисовать линию
            DrawTheRectangle()
            CurrentState = 0
        End Sub
        Private Sub Normalize(ByRef whichPoint As Point)
            ' не даем линии покидать область видимости
            If whichPoint.X < 0 Then whichPoint.X = 0
            If whichPoint.X >= BaseControl.ClientSize.Width Then whichPoint.X = BaseControl.ClientSize.Width - 1

            If whichPoint.Y < 0 Then whichPoint.Y = 0
            If whichPoint.Y >= BaseControl.ClientSize.Height Then whichPoint.Y = BaseControl.ClientSize.Height - 1

        End Sub
        Private Sub DrawTheRectangle()
            'Рисуем прямоугольник на поверхности формы
            ' или элемента управления
            Dim drawArea As Rectangle
            Dim screenStart, screenEnd As Point
            'получаем квадрат, площадь линии
            screenStart = BaseControl.PointToScreen(BasePoint)
            screenEnd = BaseControl.PointToScreen(ExtentPoint)
            drawArea.X = screenStart.X
            drawArea.Y = screenStart.Y
            drawArea.Width = screenEnd.X - screenStart.X
            drawArea.Height = screenEnd.Y - screenStart.Y
            ' рисуем применяя стиль
            ControlPaint.DrawReversibleFrame(drawArea, Color.Transparent, FrameStyle.Dashed)

        End Sub
    End Class


#End Region

#Region "Выделение, копирование, тут же вставка фрагмента экрана"

    Private Sub Form1_MouseMoveZ11(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        Try

            ToolStripTextBox17.Text = String.Format("X = {0} или {1}",
                                     MousePosition.X, e.X) & String.Format("Y = {0} или {1}",
                                               MousePosition.Y, e.Y)

        Catch ex As Exception

        End Try
    End Sub





    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Try
            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Try
            If RadioButton2.Checked = False Then
                sel = False
            Else
                sel = False
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub







    'опция копирования и вставки фрагмента экрана, определенное числовыми значениями параметров, в место, выделенное протягиванием мыши.
    Private Sub Form1_MouseDownCP(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown

        If TextBox4.Text.Trim.Length = 0 Then Exit Sub

        If TextBox5.Text.Trim.Length = 0 Then Exit Sub


        Try

            If RadioButton1.Checked = False Then
                sel = False

            Else
                sel = True

                If sel = True Then

                    If (String.IsNullOrEmpty(CType((CBool(TextBox4.Text) And CBool((TextBox5.Text)) = True), String))) Then
                        MsgBox("Введите числовые значения координат левого края прямоугольника копируемой области в поля рядом с кнопками активации")
                    Else

                        SelectionArea.Start(e.X, e.Y)
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub
    Private Sub Form1_MouseMoveCP(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        Try
            sel = True
            SelectionArea.Stretch(e.X, e.Y)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub

    Private Sub Form1_MouseUpCP(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        Try

            If TextBox4.Text.Trim.Length = 0 Then Exit Sub

            If TextBox5.Text.Trim.Length = 0 Then Exit Sub


            If RadioButton1.Checked = False Then
                sel = False

            Else
                sel = True

                If sel = True Then
                    If ([String].IsNullOrEmpty(TextBox4.Text) = True) Or ([String].IsNullOrEmpty(TextBox5.Text) = True) Then
                        MsgBox("Введите числовые значения координат левого края прямоугольника копируемой области в поля рядом с кнопками активации")

                    Else
                        SelectionArea.Finish()
                        'возвращает строку с данными в текстовое поле, в главном меню формы
                        ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()


                        Dim formGraphics As Graphics 'объект графики
                        formGraphics = CreateGraphics()

                        formGraphics.CopyFromScreen(New Point(CInt(TextBox4.Text), CType(TextBox5.Text, Integer)), New Point _
             (SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y), New Size(SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height), CopyPixelOperation.MergePaint)
                        formGraphics.Dispose()


                    End If

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub

    Private Sub Form1_MouseDownCP7(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown

        If TextBox4.Text.Trim.Length = 0 Then Exit Sub

        If TextBox5.Text.Trim.Length = 0 Then Exit Sub


        Try

            If RadioButton4.Checked = False Then
                sel = False

            Else
                sel = True

                If sel = True Then

                    If (String.IsNullOrEmpty(CType((CBool(TextBox4.Text) And CBool((TextBox5.Text)) = True), String))) Then
                        MsgBox("Введите числовые значения координат левого края прямоугольника копируемой области в поля рядом с кнопками активации")
                    Else

                        SelectionArea.Start(e.X, e.Y)
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub
    Private Sub Form1_MouseMoveCP7(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        Try
            sel = True
            SelectionArea.Stretch(e.X, e.Y)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub



    Private Sub Form1_MouseUpCP7(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        Try

            If TextBox4.Text.Trim.Length = 0 Then Exit Sub

            If TextBox5.Text.Trim.Length = 0 Then Exit Sub


            If RadioButton4.Checked = False Then
                sel = False

            Else
                sel = True

                If sel = True Then
                    If ([String].IsNullOrEmpty(TextBox4.Text) = True) Or ([String].IsNullOrEmpty(TextBox5.Text) = True) Then
                        MsgBox("Введите числовые значения координат левого края прямоугольника копируемой области в поля рядом с кнопками активации")

                    Else
                        SelectionArea.Finish()
                        'возвращает строку с данными в текстовое поле, в главном меню формы
                        ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()


                        Dim formGraphics As Graphics 'объект графики
                        formGraphics = CreateGraphics()

                        formGraphics.CopyFromScreen(New Point(CInt(TextBox4.Text), CType(TextBox5.Text, Integer)), New Point _
             (SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y), New Size(SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height), CopyPixelOperation.MergePaint)
                        formGraphics.Dispose()


                    End If

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub



#End Region

#Region "Прорисовка фигур"
    ''' <summary>
    ''' ветвление на строке меню, установка цвета
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ЦветКонтураToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ЦветКонтураToolStripMenuItem.Click, Button10.Click

        Try
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red

            If RadioButton2.Checked = False Then Exit Sub

            If ЦветКонтураToolStripMenuItem.Text() = "Цвет контура" Then
                ЦветКонтураToolStripMenuItem.BackColor = Color.Green
                Button10.BackColor = Color.Green
                ЦветКонтураToolStripMenuItem.Text() = "Рисовать"
                Button10.Text() = "Рисовать"
                Dim cdlg As New ColorDialog()
                If cdlg.ShowDialog() = DialogResult.OK Then
                    penColor = cdlg.Color
                    ToolStripTextBox5.Text = cdlg.Color.ToString()
                    ToolStripTextBox5.BackColor = cdlg.Color
                End If

            Else
                ЦветКонтураToolStripMenuItem.BackColor = Color.Beige
                Button10.BackColor = Color.Beige
                ЦветКонтураToolStripMenuItem.Text() = "Цвет контура"
                Button10.Text() = "Цвет контура"
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub ЦветToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ЦветToolStripMenuItem.Click, Button11.Click

        Try
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red

            If RadioButton2.Checked = False Then Exit Sub

            If ЦветToolStripMenuItem.Text() = "Цвет" Then
                ЦветToolStripMenuItem.BackColor = Color.Lime
                Button11.BackColor = Color.Lime
                ЦветToolStripMenuItem.Text() = "Рисовать1"
                Button11.Text() = "Рисовать1"
                Dim cdlg As New ColorDialog()
                If cdlg.ShowDialog() = DialogResult.OK Then
                    Color1 = cdlg.Color
                    ToolStripTextBox1.Text = cdlg.Color.ToString()
                    ToolStripTextBox1.BackColor = cdlg.Color
                End If

            Else
                ЦветToolStripMenuItem.BackColor = Color.Beige
                Button11.BackColor = Color.Beige
                ЦветToolStripMenuItem.Text() = "Цвет"
                Button11.Text() = "Цвет"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub CGToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CGToolStripMenuItem.Click, Button31.Click
        Try

            ЦветToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red

            RadioButton2.Checked = True

            If CGToolStripMenuItem.Text() = "CG" Then

                CGToolStripMenuItem.BackColor = Color.PaleGreen
                Button31.BackColor = Color.PaleGreen
                Button31.Text() = "Pic"
                CGToolStripMenuItem.Text() = "Pic"
            Else
                CGToolStripMenuItem.Text() = "CG"
                CGToolStripMenuItem.BackColor = Color.Beige
                Button31.BackColor = Color.Beige
                Button31.Text() = "CG"

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub
    ''' <summary>
    ''' прорисовка  выделенной области плавная ломаная, контур
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub

        Try

            If Not (ЦветКонтураToolStripMenuItem.BackColor = Color.Green) Then
                penColor = Nothing
            Else
                bDrawing = True
                Dim g As Graphics = CreateGraphics()

                PointLim = 0
                ReDim AllPoints(PointLim)
                AllPoints(PointLim).X = e.X
                AllPoints(PointLim).Y = e.Y
            End If

            If Not (ЦветToolStripMenuItem.BackColor = Color.Lime) Then

                Color1 = Nothing
            Else
                bDrawing = True
                Dim g As Graphics = CreateGraphics()

                PointLim = 0
                ReDim AllPoints(PointLim)
                AllPoints(PointLim).X = e.X
                AllPoints(PointLim).Y = e.Y
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub

        Try
            If Not (ЦветКонтураToolStripMenuItem.BackColor = Color.Green) Then

                penColor = Nothing
            Else
                If Not bDrawing Then Exit Sub
                PointLim += 1
                ReDim Preserve AllPoints(PointLim)
                AllPoints(PointLim).X = e.X
                AllPoints(PointLim).Y = e.Y
                Dim g As Graphics = CreateGraphics()
                g.SmoothingMode = SmoothingMode.AntiAlias



                Using demoPen As New Pen(penColor, CType(ToolStripTextBox6.Text, Integer))
                    demoPen.Color = penColor

                    g.DrawLine(demoPen,
                    AllPoints(PointLim - 1), AllPoints(PointLim))
                End Using
            End If



            If Not (ЦветToolStripMenuItem.BackColor = Color.Lime) Then

                Color1 = Nothing
            Else
                If Not bDrawing Then Exit Sub
                PointLim += 1
                ReDim Preserve AllPoints(PointLim)
                AllPoints(PointLim).X = e.X
                AllPoints(PointLim).Y = e.Y
                Dim g As Graphics = CreateGraphics()
                g.SmoothingMode = SmoothingMode.AntiAlias



                Using demoPen As New Pen(penColor, CType(ToolStripTextBox6.Text, Integer))
                    demoPen.Color = penColor

                    g.DrawLine(demoPen,
                    AllPoints(PointLim - 1), AllPoints(PointLim))
                End Using
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    ''' <summary>
    ''' прорисовка произвольной линии контуром для выделения произвольной области с заполнением цветом
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp

        Try
            If Not (ЦветКонтураToolStripMenuItem.BackColor = Color.Green) Then
                penColor = Nothing
            Else
                bDrawing = False
                If PointLim < 1 Then Exit Sub

                Dim g As Graphics = CreateGraphics()
                g.SmoothingMode = SmoothingMode.AntiAlias


                Dim br As New SolidBrush(Color1)

                g.FillClosedCurve(br, AllPoints)

                Using demoPen As New Pen(penColor, CType(ToolStripTextBox6.Text, Integer))
                    demoPen.Color = penColor
                    g.DrawLines(demoPen, AllPoints)
                End Using
                g.Dispose()

                g.Dispose()
            End If


            If Not (ЦветToolStripMenuItem.BackColor = Color.Lime) Then

                Color1 = Nothing
            Else
                bDrawing = False
                If PointLim < 1 Then Exit Sub

                Dim g As Graphics = CreateGraphics()
                g.SmoothingMode = SmoothingMode.AntiAlias


                Dim br As New SolidBrush(Color1)

                g.FillClosedCurve(br, AllPoints)

                Using demoPen As New Pen(penColor, CType(ToolStripTextBox6.Text, Integer))
                    demoPen.Color = penColor
                    g.DrawLines(demoPen, AllPoints)
                End Using
                g.Dispose()



            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    ''' <summary>
    ''' прорисовка прямоугольника
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_MouseDown1(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub


        Try
            If e.Button = MouseButtons.Left Then


                If Not (Button1.BackColor = Color.Green) Then
                    ComboBox1.SelectedItem = Nothing

                Else

                    'возвращает строку с данными в текстовое поле, в главном меню формы
                    ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()

                    SelectionArea.Start(e.X, e.Y)

                End If

            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub
    Private Sub Form1_MouseMove1(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub


        Try

            If e.Button = MouseButtons.Left Then
                If Not (Button1.BackColor = Color.Green) Then
                    ComboBox1.SelectedItem = Nothing

                Else

                    SelectionArea.Stretch(e.X, e.Y)
                End If

            End If




        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub

    Private Sub Form1_MouseUp1(sender As Object, e As MouseEventArgs) Handles Me.MouseUp



        Try
            If e.Button = MouseButtons.Left Then

                If Not (Button1.BackColor = Color.Green) Then
                    ComboBox1.SelectedItem = Nothing

                Else

                    SelectionArea.Finish()

                    'возвращает строку с данными в текстовое поле, в главном меню формы
                    ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()


                    If ([String].IsNullOrEmpty(TextBox1.Text) = True) Then
                        MsgBox("Введите числовое значение размера кисти")

                    Else
                        If e.Button = MouseButtons.None Then SelectionArea.Finish()

                        Dim formGraphics As Graphics 'Форма графического объекта 
                        formGraphics = CreateGraphics()

                        formGraphics.SmoothingMode = SmoothingMode.AntiAlias


                        Dim demoPen As New Pen(CType(ComboBox1.SelectedItem, Color), CType(TextBox1.Text, Single))

                        formGraphics.DrawRectangle(demoPen, SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y, SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height)
                        formGraphics.Dispose()
                    End If

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub
    ''' <summary>
    ''' прорисовка эллипса
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_MouseDown2(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub
        Try
            If e.Button = MouseButtons.Left Then

                If Not (Button2.BackColor = Color.DarkGreen) Then
                    ComboBox2.SelectedItem = Nothing
                Else
                    SelectionArea.Start(e.X, e.Y)
                    ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Form1_MouseMove2(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub

        Try
            If e.Button = MouseButtons.Left Then
                If Not (Button2.BackColor = Color.DarkGreen) Then
                    ComboBox2.SelectedItem = Nothing
                Else
                    SelectionArea.Stretch(e.X, e.Y)
                    ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Form1_MouseUp2(sender As Object, e As MouseEventArgs) Handles Me.MouseUp


        Try
            If e.Button = MouseButtons.Left Then
                If Not (Button2.BackColor = Color.DarkGreen) Then
                    ComboBox2.SelectedItem = Nothing
                Else
                    SelectionArea.Finish()
                    'возвращает строку с данными в текстовое поле, в главном меню формы
                    ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()


                    If ([String].IsNullOrEmpty(TextBox2.Text()) = True) Then
                        MsgBox("Введите числовое значение толщины прорисовки")

                    Else
                        Dim formGraphics As Graphics 'Форма графического объекта 
                        formGraphics = CreateGraphics()
                        formGraphics.SmoothingMode = SmoothingMode.AntiAlias

                        Dim demoPen As New Pen(CType(ComboBox2.SelectedItem, Color), CType(TextBox2.Text(), Single))

                        formGraphics.DrawEllipse(demoPen, SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y, SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height)
                        formGraphics.Dispose()

                    End If

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub
    ''' <summary>
    ''' прорисовка заполнененного цветом прямоугольника
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_MouseDown3(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub

        Try

            If e.Button = MouseButtons.Left Then




                If Not (Button3.BackColor = Color.GreenYellow) Then
                    ComboBox3.SelectedItem = Nothing
                Else
                    SelectionArea.Start(e.X, e.Y)
                End If
            End If


        Catch ex As Exception

        End Try

    End Sub
    Private Sub Form1_MouseMove3(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub
        sel = True

        Try
            If e.Button = MouseButtons.Left Then


                If sel = True Then
                    If Not (Button3.BackColor = Color.GreenYellow) Then
                        ComboBox3.SelectedItem = Nothing
                    Else
                        SelectionArea.Stretch(e.X, e.Y)
                    End If
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub
    Private Sub Form1_MouseUp3(sender As Object, e As MouseEventArgs) Handles Me.MouseUp



        Try
            If e.Button = MouseButtons.Left Then

                If Not (Button3.BackColor = Color.GreenYellow) Then
                    ComboBox3.SelectedItem = Nothing
                Else
                    SelectionArea.Finish()
                    'возвращает строку с данными в текстовое поле, в главном меню формы
                    ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()

                    Dim formGraphics As Graphics 'Форма графического объекта 
                    formGraphics = CreateGraphics()

                    formGraphics.SmoothingMode = SmoothingMode.AntiAlias

                    demoBrush = New SolidBrush(CType(ComboBox3.SelectedItem, Color))

                    formGraphics.FillRectangle(demoBrush, SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y, SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height)
                    ' 10 x 10 пикселей - размер сплошного элипса
                    ' e.X, e.Y - координаты указателя мыши
                    formGraphics.Dispose()
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub
    ''' <summary>
    ''' прорисовка заполненного цветом эллипса
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_MouseDown4(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub


        Try
            If e.Button = MouseButtons.Left Then

                If Not (Button4.BackColor = Color.LawnGreen) Then
                    ComboBox4.SelectedItem = Nothing
                Else
                    SelectionArea.Start(e.X, e.Y)
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub
    Private Sub Form1_MouseMove4(sender As Object, e As MouseEventArgs) Handles Me.MouseMove


        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub

        Try
            If e.Button = MouseButtons.Left Then
                If Not (Button4.BackColor = Color.LawnGreen) Then
                    ComboBox4.SelectedItem = Nothing
                Else
                    SelectionArea.Stretch(e.X, e.Y)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub
    Private Sub Form1_MouseUp4(sender As Object, e As MouseEventArgs) Handles Me.MouseUp


        Try
            If e.Button = MouseButtons.Left Then

                If Not (Button4.BackColor = Color.LawnGreen) Then
                    ComboBox4.SelectedItem = Nothing
                Else
                    SelectionArea.Finish()
                    'возвращает строку с данными в текстовое поле, в главном меню формы
                    ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()

                    Dim formGraphics As Graphics 'Форма графического объекта 
                    formGraphics = CreateGraphics()
                    formGraphics.SmoothingMode = SmoothingMode.AntiAlias


                    demoBrush = New SolidBrush(CType(ComboBox4.SelectedItem, Color))



                    formGraphics.FillEllipse(demoBrush, SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y, SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height)
                    ' 10 x 10 пикселей - размер сплошного элипса
                    ' e.X, e.Y - координаты указателя мыши
                    formGraphics.Dispose()


                    formGraphics.Dispose()

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub
    ''' <summary>
    ''' опции активации простейших элементов рисования
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red
            RadioButton2.Checked = True


            If Button1.Text() = "Прямоугольник" Then
                Button1.BackColor = Color.Green
                Button1.Text() = "Рисовать"

            Else
                Button1.BackColor = Color.Beige
                Button1.Text() = "Прямоугольник"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red

            RadioButton2.Checked = True


            If Button2.Text() = "Эллипс" Then
                Button2.BackColor = Color.DarkGreen
                Button2.Text() = "Рисовать"

            Else
                Button2.BackColor = Color.Beige
                Button2.Text() = "Эллипс"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try

            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red

            RadioButton2.Checked = True


            If Button3.Text() = "Окрашенный пр." Then
                Button3.BackColor = Color.GreenYellow
                Button3.Text() = "Рисовать"

            Else
                Button3.BackColor = Color.Beige
                Button3.Text() = "Окрашенный пр."
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Try
            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red

            RadioButton2.Checked = True


            If Button4.Text() = "Окрашенный эл." Then
                Button4.BackColor = Color.LawnGreen
                Button4.Text() = "Рисовать"

            Else
                Button4.BackColor = Color.Beige
                Button4.Text() = "Окрашенный эл."
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    'Масштабирование, развертывание произвольно выделенной области градиентной заливкой.
    Private Sub Forml_MouseDownZ33(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub

        Try
            If e.Button = MouseButtons.Left Then


                If Button31.BackColor = Color.PaleGreen Then
                    bDrawing = True

                    PointLim = 0
                    ReDim AllPoints(PointLim)
                    AllPoints(PointLim).X = e.X
                    AllPoints(PointLim).Y = e.Y

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Forml_MouseMoveZ33(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub


        Try
            If e.Button = MouseButtons.Left Then

                If Button31.BackColor = Color.PaleGreen Then
                    If Not bDrawing Then Exit Sub
                    PointLim += 1
                    ReDim Preserve AllPoints(PointLim)
                    AllPoints(PointLim).X = e.X
                    AllPoints(PointLim).Y = e.Y
                    Dim g As Graphics = CreateGraphics()
                    g.SmoothingMode = SmoothingMode.AntiAlias


                    Using demoPen As New Pen(penColor, CType(ToolStripTextBox6.Text, Integer))
                        demoPen.Color = penColor

                        g.DrawLine(demoPen,
                        AllPoints(PointLim - 1), AllPoints(PointLim))
                    End Using

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Forml_MouseUpZ33(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp


        Try
            If e.Button = MouseButtons.Left Then


                If Button31.BackColor = Color.PaleGreen Then
                    bDrawing = False
                    If PointLim < 1 Then Exit Sub

                    Dim g As Graphics = CreateGraphics()


                    g.SmoothingMode = SmoothingMode.AntiAlias

                    Dim Rectangle As New Rectangle(e.X, e.Y, e.X, e.X)


                    ' Заполнение прямоугольника градиентной заливкой.
                    Dim DrawingBrush1 As New LinearGradientBrush(Rectangle, CType(ToolStripComboBox3.SelectedItem, Color), CType(ToolStripComboBox4.SelectedItem, Color), CType(ToolStripTextBox4.Text, Integer))

                    g.FillClosedCurve(DrawingBrush1, AllPoints)

                    Using demoPen As New Pen(penColor, CType(ToolStripTextBox6.Text, Integer))
                        demoPen.Color = penColor
                        g.DrawLines(demoPen, AllPoints)
                    End Using
                    g.Dispose()


                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    ' развертывание, масштабирование прямоугольника с градиентной заливкой
    Private Sub Form1_MouseDownZ(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub

        Try

            If e.Button = MouseButtons.Left Then


                If Button27.BackColor = Color.DarkOliveGreen Then

                    SelectionArea.Start(e.X, e.Y)
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub Form1_MouseMoveZ(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub
        Try
            If e.Button = MouseButtons.Left Then

                If Button27.BackColor = Color.DarkOliveGreen Then

                    SelectionArea.Stretch(e.X, e.Y)

                End If
            End If

        Catch ex As Exception

        End Try





    End Sub
    Private Sub Form1_MouseUpZ(sender As Object, e As MouseEventArgs) Handles Me.MouseUp

        If SelectionArea.Rectangle.X = 0 Then Exit Sub

        If SelectionArea.Rectangle.Y = 0 Then Exit Sub

        If SelectionArea.Rectangle.Width = 0 Then Exit Sub

        If SelectionArea.Rectangle.Height = 0 Then Exit Sub

        Try

            If e.Button = MouseButtons.Left Then

                If Button27.BackColor = Color.DarkOliveGreen Then

                    If Not (ToolStripTextBox4.Text = String.Empty) Then
                        SelectionArea.Finish()


                        'возвращает строку с данными в текстовое поле, в главном меню формы
                        ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()


                        Dim g As Graphics = CreateGraphics()
                        g.SmoothingMode = SmoothingMode.AntiAlias

                        Dim Rectangle As New Rectangle(SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y, SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height)

                        ' Заполнение прямоугольника градиентной заливкой.
                        Dim DrawingBrush1 As New LinearGradientBrush(Rectangle, CType(ToolStripComboBox3.SelectedItem, Color), CType(ToolStripComboBox4.SelectedItem, Color), CType(ToolStripTextBox4.Text, Integer))

                        g.FillRectangle(DrawingBrush1, Rectangle)


                        g.Dispose()

                    End If
                End If

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub
    'опция активации и блокировки иных опций

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Try
            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red

            RadioButton2.Checked = True


            If ([String].IsNullOrEmpty(ToolStripTextBox4.Text) = True) Then
                MsgBox("Введите угол градиента в текстовое поле меню")
            Else


                If Button27.Text() = "DrGRRect" Then
                    Button27.BackColor = Color.DarkOliveGreen

                    Button27.Text() = "Рисовать"

                Else
                    Button27.BackColor = Color.Beige
                    Button27.Text() = "DrGRRect"
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_MouseDownZ1(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub


        Try

            If e.Button = MouseButtons.Left Then

                If Button28.BackColor = Color.DarkSeaGreen Then



                    SelectionArea.Start(e.X, e.Y)
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub
    Private Sub Form1_MouseMoveZ1(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub



        Try
            If e.Button = MouseButtons.Left Then

                If Button28.BackColor = Color.DarkSeaGreen Then

                    SelectionArea.Stretch(e.X, e.Y)
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub
    Private Sub Form1_MouseUpZ1(sender As Object, e As MouseEventArgs) Handles Me.MouseUp


        If SelectionArea.Rectangle.X = 0 Then Exit Sub

        If SelectionArea.Rectangle.Y = 0 Then Exit Sub

        If SelectionArea.Rectangle.Width = 0 Then Exit Sub

        If SelectionArea.Rectangle.Height = 0 Then Exit Sub



        If ToolStripTextBox4.Text.Trim.Length = 0 Then Exit Sub

        Try
            If e.Button = MouseButtons.Left Then


                If Button28.BackColor = Color.DarkSeaGreen Then

                    SelectionArea.Finish()
                    'возвращает строку с данными в текстовое поле, в главном меню формы
                    ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()

                    If ([String].IsNullOrEmpty(ToolStripTextBox4.Text) = True) Then
                        MsgBox("Введите угол градиента в текстовое поле меню")
                    Else

                        Dim g As Graphics = CreateGraphics()
                        g.SmoothingMode = SmoothingMode.AntiAlias

                        Dim Rectangle As New Rectangle(SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y, SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height)

                        ' Заполнение прямоугольника градиентной заливкой.
                        Dim DrawingBrush1 As New LinearGradientBrush(Rectangle, CType(ToolStripComboBox3.SelectedItem, Color), CType(ToolStripComboBox4.SelectedItem, Color), CType(ToolStripTextBox4.Text, Integer))

                        g.FillEllipse(DrawingBrush1, Rectangle)


                        g.Dispose()

                    End If
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub
    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click



        Try
            If ([String].IsNullOrEmpty(ToolStripTextBox4.Text) = True) Then
                MsgBox("Введите угол градиента в текстовое поле меню")
            Else


                ЦветКонтураToolStripMenuItem.BackColor = Color.Red
                ЦветToolStripMenuItem.BackColor = Color.Red
                Button1.BackColor = Color.Red
                Button2.BackColor = Color.Red
                Button3.BackColor = Color.Red
                Button4.BackColor = Color.Red
                Button10.BackColor = Color.Red
                Button11.BackColor = Color.Red
                Button27.BackColor = Color.Red
                Button29.BackColor = Color.Red
                Button30.BackColor = Color.Red
                Button31.BackColor = Color.Red
                CGToolStripMenuItem.BackColor = Color.Red
                Button33.BackColor = Color.Red
                Button34.BackColor = Color.Red
                Button35.BackColor = Color.Red
                Button36.BackColor = Color.Red

                RadioButton2.Checked = True


                If Button28.Text() = "DrGREll" Then
                    Button28.BackColor = Color.DarkSeaGreen

                    Button28.Text() = "Рисовать"

                Else
                    Button28.BackColor = Color.Beige
                    Button28.Text() = "DrGREll"
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    ' масштабирование прямоугольника с условно произвольным градиентом цветов
    Private Sub Form1_MouseDownZ2(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub

        Try
            If e.Button = MouseButtons.Left Then

                If Button29.BackColor = Color.LightSeaGreen Then
                    SelectionArea.Start(e.X, e.Y)
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Form1_MouseMoveZ2(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub

        Try
            If e.Button = MouseButtons.Left Then

                If Button29.BackColor = Color.LightSeaGreen Then
                    SelectionArea.Stretch(e.X, e.Y)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Form1_MouseUpZ2(sender As Object, e As MouseEventArgs) Handles Me.MouseUp

        If SelectionArea.Rectangle.X = 0 Then Exit Sub

        If SelectionArea.Rectangle.Y = 0 Then Exit Sub

        If SelectionArea.Rectangle.Width = 0 Then Exit Sub

        If SelectionArea.Rectangle.Height = 0 Then Exit Sub


        Try

            If e.Button = MouseButtons.Left Then


                If Button29.BackColor = Color.LightSeaGreen Then

                    SelectionArea.Finish()
                    'возвращает строку с данными в текстовое поле, в главном меню формы
                    ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()


                    Dim g As Graphics = CreateGraphics()
                    g.SmoothingMode = SmoothingMode.AntiAlias

                    Dim Rectangle As New Rectangle(SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y, SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height)

                    ' Заполнение прямоугольника градиентной заливкой.
                    Using DrawingBrush1 As New LinearGradientBrush(Rectangle, Color.FromArgb(255, m_Random.Next(255), m_Random.Next(255), m_Random.Next(255)), Color.FromArgb(255, m_Random.Next(255), m_Random.Next(255), m_Random.Next(255)), m_Random.Next(900))

                        g.FillRectangle(DrawingBrush1, Rectangle)
                    End Using




                    g.Dispose()

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub
    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        Try

            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red

            RadioButton2.Checked = True


            If Button29.Text() = "DrGrRR" Then
                Button29.BackColor = Color.LightSeaGreen

                Button29.Text() = "Рисовать"

            Else
                Button29.BackColor = Color.Beige
                Button29.Text() = "DrGrRR"
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    'масштабирование овала с условно произвольным градиентом цветов

    Private Sub Form1_MouseDownZ3(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub

        Try
            If Button30.BackColor = Color.Olive Then
                SelectionArea.Start(e.X, e.Y)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Form1_MouseMoveZ3(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub


        Try
            If Button30.BackColor = Color.Olive Then
                SelectionArea.Stretch(e.X, e.Y)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub Form1_MouseUpZ3(sender As Object, e As MouseEventArgs) Handles Me.MouseUp

        If SelectionArea.Rectangle.X = 0 Then Exit Sub

        If SelectionArea.Rectangle.Y = 0 Then Exit Sub

        If SelectionArea.Rectangle.Width = 0 Then Exit Sub

        If SelectionArea.Rectangle.Height = 0 Then Exit Sub


        Try
            If Button30.BackColor = Color.Olive Then

                SelectionArea.Finish()
                'возвращает строку с данными в текстовое поле, в главном меню формы
                ToolStripTextBox8.Text = SelectionArea.Rectangle.ToString()


                Dim g As Graphics = CreateGraphics()
                g.SmoothingMode = SmoothingMode.AntiAlias

                Dim Rectangle As New Rectangle(SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y, SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height)

                ' Заполнение прямоугольника градиентной заливкой.
                Dim DrawingBrush1 As New LinearGradientBrush(Rectangle, Color.FromArgb(255, m_Random.Next(255), m_Random.Next(255), m_Random.Next(255)), Color.FromArgb(255, m_Random.Next(255), m_Random.Next(255), m_Random.Next(255)), m_Random.Next(900))

                g.FillEllipse(DrawingBrush1, Rectangle)


                g.Dispose()



            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub
    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        Try

            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button31.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red
            RadioButton2.Checked = True

            If Button30.Text() = "DrGrElR" Then
                Button30.BackColor = Color.Olive

                Button30.Text() = "Рисовать"

            Else
                Button30.BackColor = Color.Beige
                Button30.Text() = "DrGrElR"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    ' масштабирование окрашенного прямоугольника
    Private Sub Form1_MouseDownZ7(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown


        Try

            If Button33.BackColor = Color.OliveDrab Then
                If e.Button = MouseButtons.Left Then

                    ' Если нажата кнопка мыши-MouseDown, то рисовать:
                    pt = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Form1_MouseZ7(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove

        If ToolStripTextBox7.Text.Trim.Length = 0 Then Exit Sub

        If ToolStripTextBox10.Text.Trim.Length = 0 Then Exit Sub


        Try

            Dim a As Integer = CInt(ToolStripTextBox7.Text)
            Dim b As Integer = CInt(ToolStripTextBox10.Text)

            pt = True
            If Button33.BackColor = Color.OliveDrab Then

                If (String.IsNullOrEmpty(CType((CBool(ToolStripTextBox7.Text) And CBool((ToolStripTextBox10.Text)) = True), String))) Then
                    MsgBox("Введите числовые параметры фигуры прорисовки")

                Else
                    If e.Button = MouseButtons.Left Then
                        'Рисование прямоугольника, если нажата кнопка мыши:
                        If pt = True Then
                            ' Рисовать прямоугольник в точке e.X, e.Y :
                            Dim g As Graphics = CreateGraphics()
                            Dim Rectangle As New Rectangle(e.X, e.Y, a, b)

                            Dim DrawingBrush1 As New LinearGradientBrush(Rectangle, Color.FromArgb(255, m_Random.Next(255), m_Random.Next(255), m_Random.Next(255)), Color.FromArgb(255, m_Random.Next(255), m_Random.Next(255), m_Random.Next(255)), m_Random.Next(900))

                            g.FillRectangle(DrawingBrush1, Rectangle)



                            ' e.X, e.Y - координаты указателя мыши
                        End If

                    End If

                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Form1_MouseUpZ7(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        Try
            ' Если кнопку мыши отпустили, то НЕ рисовать:
            pt = False
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Try
            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            Button34.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red

            RadioButton2.Checked = True

            If Button33.Text() = "Re" Then
                Button33.BackColor = Color.OliveDrab

                Button33.Text() = "Рc"

            Else
                Button33.BackColor = Color.Beige
                Button33.Text() = "Re"
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub






    Private Sub Form1_MouseDownZ75(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown

        Try
            If Button34.BackColor = Color.LightGreen Then

                If e.Button = MouseButtons.Left Then

                    ' Если нажата кнопка мыши-MouseDown, то рисовать:
                    pt = True
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub Form1_MouseZ75(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove

        If ToolStripTextBox7.Text.Trim.Length = 0 Then Exit Sub

        If ToolStripTextBox10.Text.Trim.Length = 0 Then Exit Sub



        Try

            Dim a As Integer = CInt(ToolStripTextBox7.Text)
            Dim b As Integer = CInt(ToolStripTextBox10.Text)

            pt = True
            If Button34.BackColor = Color.LightGreen Then

                If (String.IsNullOrEmpty(CType((CBool(ToolStripTextBox7.Text) And CBool((ToolStripTextBox10.Text)) = True), String))) Then
                    MsgBox("Введите числовые параметры фигуры прорисовки")

                Else
                    If e.Button = MouseButtons.Left Then
                        'Рисование прямоугольника, если нажата кнопка мыши:
                        If pt = True Then
                            ' Рисовать прямоугольник в точке e.X, e.Y :
                            Dim g As Graphics = CreateGraphics()
                            Dim Rectangle As New Rectangle(e.X, e.Y, a, b)



                            Dim DrawingBrush1 As New LinearGradientBrush(Rectangle, Color.FromArgb(255, m_Random.Next(255), m_Random.Next(255), m_Random.Next(255)), Color.FromArgb(255, m_Random.Next(255), m_Random.Next(255), m_Random.Next(255)), m_Random.Next(900))

                            g.FillEllipse(DrawingBrush1, Rectangle)



                            ' e.X, e.Y - координаты указателя мыши
                        End If

                    End If

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Form1_MouseUpZ75(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        Try
            ' Если кнопку мыши отпустили, то НЕ рисовать:
            pt = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Try
            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red

            RadioButton2.Checked = True



            If Button34.Text() = "Ell" Then
                Button34.BackColor = Color.LightGreen

                Button34.Text() = "Рc"

            Else
                Button34.BackColor = Color.Beige
                Button34.Text() = "Ell"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub






    Private Sub Form1_MouseDownZ77(sender As Object, e As MouseEventArgs) Handles Me.MouseDown


        Try
            If e.Button = MouseButtons.Left Then
                If Button35.BackColor = Color.MediumSeaGreen Then
                    ' Если нажата кнопка мыши-MouseDown, то рисовать:
                    pt = True
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub
    Private Sub Form1_MouseUpZ77(sender As Object, e As MouseEventArgs) Handles Me.MouseUp


        Try
            ' Если кнопку мыши отпустили-MouseUp, не рисовать:
            pt = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub

    Private Sub Form1_MouseMoveZ77(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If ToolStripTextBox7.Text.Trim.Length = 0 Then Exit Sub
        If ToolStripTextBox10.Text.Trim.Length = 0 Then Exit Sub
        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub


        Try

            Dim a As Integer = CInt(ToolStripTextBox7.Text)
            Dim b As Integer = CInt(ToolStripTextBox10.Text)
            If Button35.BackColor = Color.MediumSeaGreen Then

                If (String.IsNullOrEmpty(CType((CBool(ToolStripTextBox7.Text) And CBool((ToolStripTextBox9.Text)) And CBool((ToolStripTextBox10.Text)) = True), String))) Then
                    MsgBox("Введите числовые параметры фигуры прорисовки")

                Else
                    If e.Button = MouseButtons.Left Then
                        'Рисование прямоугольника, если нажата кнопка мыши:
                        If pt = True Then

                            ' e.X, e.Y - координаты указателя мыши

                            Dim Rectangle As New Rectangle(e.X, e.Y, a, b)


                            Dim g As Graphics = CreateGraphics()
                            g.SmoothingMode = SmoothingMode.AntiAlias

                            'точки ротации
                            Dim rotatePoint As New PointF(e.X, e.Y)
                            ' создание объекта матрицы преобразований
                            Dim Matrix1 As New Matrix
                            'таким же образом и здесь можно заменить значения аргумента угла поворота на переменную элемента управления.
                            Matrix1.RotateAt(m_Random.Next(CInt(ToolStripTextBox9.Text)), rotatePoint, MatrixOrder.Append)
                            ' прорисовка результатов преобразований
                            g.Transform = Matrix1


                            ' Заполнение прямоугольника градиентной заливкой.
                            Dim DrawingBrush1 As New LinearGradientBrush(Rectangle, CType(ToolStripComboBox3.SelectedItem, Color), CType(ToolStripComboBox4.SelectedItem, Color), CType(ToolStripTextBox4.Text, Integer))
                            g.FillRectangle(DrawingBrush1, Rectangle)


                        End If

                    End If

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Try

            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button36.BackColor = Color.Red

            RadioButton2.Checked = True


            If Button35.Text() = "RC" Then
                Button35.BackColor = Color.MediumSeaGreen

                Button35.Text() = "Рc"

            Else
                Button35.BackColor = Color.Beige
                Button35.Text() = "RC"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub


    Private Sub Form1_MouseDownZ777(sender As Object, e As MouseEventArgs) Handles Me.MouseDown

        Try
            If e.Button = MouseButtons.Left Then
                If Button36.BackColor = Color.MediumSpringGreen Then
                    ' Если нажата кнопка мыши-MouseDown, то рисовать:
                    pt = True
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub
    Private Sub Form1_MouseUpZ777(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        Try
            ' Если кнопку мыши отпустили-MouseUp, не рисовать:
            pt = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub

    Private Sub Form1_MouseMoveZ777(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If ToolStripTextBox7.Text.Trim.Length = 0 Then Exit Sub
        If ToolStripTextBox10.Text.Trim.Length = 0 Then Exit Sub
        If e.X = 0 Then Exit Sub
        If e.Y = 0 Then Exit Sub

        Try
            Dim a As Integer = CInt(ToolStripTextBox7.Text)
            Dim b As Integer = CInt(ToolStripTextBox10.Text)
            If Button36.BackColor = Color.MediumSpringGreen Then

                If (String.IsNullOrEmpty(CType((CBool(ToolStripTextBox7.Text) And (CBool(ToolStripTextBox10.Text) And CBool((ToolStripTextBox11.Text))) = True), String))) Then
                    MsgBox("Введите числовые параметры фигур прорисовки")

                Else
                    If e.Button = MouseButtons.Left Then
                        'Рисование прямоугольника, если нажата кнопка мыши:
                        If pt = True Then

                            ' e.X, e.Y - координаты указателя мыши

                            Dim Rectangle As New Rectangle(e.X, e.Y, a, b)


                            Dim g As Graphics = CreateGraphics()
                            g.SmoothingMode = SmoothingMode.AntiAlias

                            'точки ротации
                            Dim rotatePoint As New PointF(e.X, e.Y)
                            ' создание объекта матрицы
                            Dim Matrix1 As New Matrix
                            'таким же образом и здесь можно заменить значения аргумента угла поворота на переменную элемента управления.
                            Matrix1.RotateAt(m_Random.Next(CInt(ToolStripTextBox11.Text)), rotatePoint, MatrixOrder.Append)
                            ' рисование преобразования
                            g.Transform = Matrix1


                            ' Заполнение прямоугольника градиентной заливкой.
                            Dim DrawingBrush1 As New LinearGradientBrush(Rectangle, CType(ToolStripComboBox3.SelectedItem, Color), CType(ToolStripComboBox4.SelectedItem, Color), CType(ToolStripTextBox4.Text, Integer))
                            g.FillEllipse(DrawingBrush1, Rectangle)


                        End If

                    End If

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Try

            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            RadioButton2.Checked = True


            If Button36.Text() = "EllC" Then
                Button36.BackColor = Color.MediumSpringGreen

                Button36.Text() = "Рc"

            Else
                Button36.BackColor = Color.Beige
                Button36.Text() = "EllC"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub



#End Region

#Region "Опции меню, кластера прорисовки и обслуживания"


    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click

        If originalImage IsNot Nothing Then
            originalImage.Dispose()

        End If

        If currentImage IsNot Nothing Then
            currentImage.Dispose()

        End If


        Try
            'Создание диалога открытия пяти форматов файлов изображений и выведение контента на экран в окно активного документа. 
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                ' Open Image
                originalImage = CType(Image.FromFile(OpenFileDialog1.FileName), Bitmap)
                currentImage = CType(originalImage.Clone(), Image)
                Dim Графика As Graphics = CreateGraphics()
                Графика = Delate()
                Графика.DrawImage(originalImage, x:=0, y:=0)

                Title()

                OpenToolStripMenuItem.Enabled = False


            End If
        Catch Exc As FileNotFoundException
            MessageBox.Show("Нет такого файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Sub Title()

        Try
            'блок создания сообщения о имени открытого файла в главном меню программы.
            Dim frm As Form1
            If m_FileLoaded Then
                frm = New Form1
            Else
                frm = Me
            End If


            frm.Text = "Graphic Editor Draw With Mouse [" & New FileInfo(OpenFileDialog1.FileName).FullName & "]"

            frm.m_FileLoaded = True


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try

            If Button7.BackColor = Color.WhiteSmoke Then
                Picture()
                Button7.BackColor = Color.Gold
                Button7.Text = "Picture In"
            Else
                Button7.BackColor = Color.WhiteSmoke
                Button7.Text = "Create"
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub CreateToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CreateToolStripMenuItem1.Click
        Try
            Picture()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    ''' <summary>
    ''' создание изображения
    ''' </summary>
    Private Sub Picture()
        Try
            'Обслуживание буфера обмена
            My.Computer.Clipboard.Clear()

            'Копирование в буфер обмена изображения окна активного документа 
            keybd_event(44, 1, 0, 0)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    ''' <summary>
    ''' сохранение изображения
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SaveImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveImageToolStripMenuItem.Click
        Try
            SaveImage()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Sub SaveImage()
        Try
            If (([String].IsNullOrEmpty(ToolStripTextBox2.Text) = True) Or ([String].IsNullOrEmpty(ToolStripTextBox3.Text))) Then
                MsgBox("Введите числовые параметры")
            Else
                'Объявление и декларация целочисленных переменных для выбора местоположения прорисовки картинки
                Dim a As Integer = CInt(ToolStripTextBox2.Text)
                Dim b As Integer = CInt(ToolStripTextBox3.Text)
                'Условное утверждение управляющее выводом изображение из буфера обмена
                If My.Computer.Clipboard.ContainsImage() Then
                    'Объявление и декларация переменной битмапа 
                    Dim image As New Bitmap(CType(ToolStripComboBox1.SelectedItem, Integer), CType(ToolStripComboBox2.SelectedItem, Integer))
                    ' Создание объекта графика, присвоение значения исполнения
                    Dim графика As Graphics = Graphics.FromImage(image)

                    'Объявление и декларация переменной изображения 
                    Dim изображение As Image
                    ' присвоение значения исполнения переменной изображения, извлечение картинки из буфера обмена в исполняемое значение переменной
                    изображение = My.Computer.Clipboard.GetImage()
                    ' метод прорисовки с аргументами изображения и локализации изображения (здесь, в границах битмапа)
                    графика.DrawImage(изображение, x:=a, y:=b)
                    'Открытие диалога сохранения существующего файла изображений в пяти форматах: bmp, gif, tiff, jpeg, png. 
                    If SaveFileDialog1.ShowDialog() = DialogResult.OK Then


                        ' присвоение значения исполнения переменной изображения, извлечение картинки из буфера обмена в исполняемое значение переменной
                        изображение = My.Computer.Clipboard.GetImage()
                        ' метод прорисовки с аргументами изображения и локализации изображения (здесь, в границах битмапа)
                        графика.DrawImage(изображение, x:=a, y:=b)
                        'метод прорисовки изображения, битмапа
                        графика.DrawImage(image, 0, 0)
                        ' сохранение изображения битмапа с белым фоном и картинкой 
                        image.Save(SaveFileDialog1.FileName, GetImageFormat())
                        image.Dispose()
                    End If
                Else
                    MsgBox("Нет изображений в буфере обмена")
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    ''' <summary>
    ''' процедура вызова возврата изображения из буфера обмена
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnToolStripMenuItem.Click
        Try
            ReturnPicture()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub


    ''' <summary>
    ''' освежить клиентскую область
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click
        Try
            Refresh()
            ToolStripButton3.BackColor = Color.Red
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub


    Private Sub OpacityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click, OpacityToolStripMenuItem.Click
        Try
            'прозрачность формы
            Beep()
            Timer1.Enabled = Not Timer1.Enabled
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    ''' <summary>
    ''' восстановление непрозрачности формы
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Opacity1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click, Opacity1ToolStripMenuItem.Click
        Try
            Timer1.Enabled = False

            Form3.Opacity = 1

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub


    ''' <summary>
    ''' цикл повышения и понижения прозрачности формы
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try

            If Form3.Opacity <= 0 Or Form3.Opacity >= 1 Then
                ' Stop
                OpTy = -OpTy

            End If
            Form3.Opacity += OpTy
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    ''' <summary>
    ''' функция очистки клинской области
    ''' </summary>
    ''' <returns></returns>
    Function Delate() As Graphics
        Dim bmp As Bitmap
        bmp = New Bitmap(Width, Height)
        Dim G As Graphics
        BackgroundImage = bmp
        G = Graphics.FromImage(bmp)
        Return G
    End Function

    ''' <summary>
    ''' градиентная заливка клиенской области рисования
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click, GradientBCkToolStripMenuItem.Click, Button21.Click

        If ToolStripTextBox4.Text.Trim.Length = 0 Then Exit Sub

        Try

            If ([String].IsNullOrEmpty(ToolStripTextBox4.Text) = True) Then
                MsgBox("Введите угол градиента в текстовое поле меню")
            Else
                Refresh()

                ToolStripButton3.BackColor = Color.Green

                Dim Rectangle As New Rectangle(5, 5, ClientRectangle.Width - 10, ClientRectangle.Height - 10)


                Dim g As Graphics = CreateGraphics()

                g.SmoothingMode = SmoothingMode.AntiAlias

                ' Прорисовка корпуса прямоугольника.
                Dim DrawingPen As New Pen(Color.Red, 2)

                g.DrawRectangle(DrawingPen, Rectangle)


                ' Заполнение прямоугольника градиентной заливкой.
                Dim DrawingBrush1 As New LinearGradientBrush(Rectangle, CType(ToolStripComboBox3.SelectedItem, Color), CType(ToolStripComboBox4.SelectedItem, Color), CType(ToolStripTextBox4.Text, Integer))
                g.FillRectangle(DrawingBrush1, Rectangle)

                g.Dispose()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Try
            Close()
            OpenToolStripMenuItem.Enabled = True


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    'фрагмент кода студии
    ''' <summary>
    ''' преобразование координат мыши в экранные координаты, вывод значений в текст
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Me_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Try
            Dim screenPoint As Point
            screenPoint = PointToScreen(New Point(e.X, e.Y))
            'вывод значения

            Label5.Text() = screenPoint.ToString

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub

    ''' <summary>
    ''' перемещение производного окна программы в матричной области мышью в любой точке клентской области
    ''' </summary>
    Dim mouseOffset As Point

    Private Sub Me_MouseDown1(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        Try
            mouseOffset = New Point(-e.X, -e.Y)
        Catch ex As ArgumentException
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    ''' <summary>
    ''' перемещение окна правой кнопкой мыши для всей клиентской области
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Me_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        Try
            If e.Button = MouseButtons.Right Then
                ContrlMouse = True
                Dim mousePos = MousePosition
                mousePos.Offset(mouseOffset.X, mouseOffset.Y)
                Location = mousePos

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub InvisibleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvisibleToolStripMenuItem.Click

        Try
            If Not WindowState = FormWindowState.Maximized Then Exit Sub



            If InvisibleToolStripMenuItem.Text() = "Invisible" Then
                FormBorderStyle = FormBorderStyle.None
                ToolStrip1.Visible = False
                TabControl1.Visible = False
                InvisibleToolStripMenuItem.Text() = "Visible"
            Else
                ToolStrip1.Visible = True
                TabControl1.Visible = True
                FormBorderStyle = FormBorderStyle.Fixed3D
                InvisibleToolStripMenuItem.Text() = "Invisible"

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub



    Private Sub InvisibleAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvisibleAllToolStripMenuItem.Click


        Try

            If Button20.BackColor = Color.Lime = False Then Exit Sub
            If Not WindowState = FormWindowState.Maximized Then Exit Sub




            FormBorderStyle = FormBorderStyle.None
            MenuStrip1.Visible = False
            ToolStrip1.Visible = False
            TabControl1.Visible = False

            Form3.FormBorderStyle = FormBorderStyle.None
            Form3.MenuStrip1.Visible = False
            Form3.ToolStrip1.Visible = False


            PictureBox1.Location = New Point(0, 50)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    ''' <summary>
    ''' создание изображения двойным кликом правой кнопкой мыши по окну картинок
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub PictureBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDoubleClick
        Try
            If e.Button = MouseButtons.Right Then
                ContrlMouse1 = True


                SaveImage1()

                InsertImage()

                Button15.Enabled = False


            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub
    ''' <summary>
    ''' редукция всех элементов управления на форме, состояние: "невидимы".
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PictureBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseClick
        Try
            If e.Button = MouseButtons.Left Then

                ContrlMouse10 = True

                FormBorderStyle = FormBorderStyle.Fixed3D
                MenuStrip1.Visible = True
                ToolStrip1.Visible = True
                TabControl1.Visible = True

                Form3.FormBorderStyle = FormBorderStyle.Fixed3D
                Form3.MenuStrip1.Visible = True
                Form3.ToolStrip1.Visible = False


                PictureBox1.Location = New Point(5, 638)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub ОПрограммеToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ОПрограммеToolStripMenuItem1.Click
        Try
            AboutBox1.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    ''' <summary>
    ''' ползунок прозрачности формы.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar1.Scroll
        Try
            'прозрачность формы, горизонтальный ползунок
            Form3.Opacity = 0.1 + HScrollBar1.Value / 100

            Label6.Text = "Opacity " & Form3.Opacity.ToString

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    ''' <summary>
    ''' процедура очистки и возврата к изначальному виду клинской области, закрытие окна и графического файла. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Refresh()
            Delate()
            Close()

            ToolStripButton3.BackColor = Color.Red
            If originalImage IsNot Nothing Then
                originalImage.Dispose()
                Close()
            Else
                Exit Sub
            End If

            If currentImage IsNot Nothing Then
                currentImage.Dispose()

                Close()
            Else
                Exit Sub

            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            SaveImage()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            ReturnPicture()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    ''' <summary>
    ''' процедура возврата изображения из буфера обмена
    ''' </summary>
    Private Sub ReturnPicture()
        Try
            'Копирование в буфер обмена изображения окна активного документа 
            keybd_event(44, 1, 0, 0)

            If My.Computer.Clipboard.ContainsImage() Then
                Dim изображение As Image
                изображение = My.Computer.Clipboard.GetImage()
                Dim Графика As Graphics = CreateGraphics()
                Графика = Delate()
                Графика.DrawImage(изображение, CInt(ToolStripTextBox2.Text()), CInt(ToolStripTextBox3.Text))
                Графика.Dispose()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            If Not WindowState = FormWindowState.Maximized Then Exit Sub

            If Button12.Text() = "Some Invisible" Then
                FormBorderStyle = FormBorderStyle.None
                ToolStrip1.Visible = False
                MenuStrip1.Visible = False

                Button12.Text() = "Visible"
            Else
                ToolStrip1.Visible = True
                MenuStrip1.Visible = True

                FormBorderStyle = FormBorderStyle.Fixed3D
                Button12.Text() = "Some Invisible"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Try

            If Button14.Text() = "Text" Then
                Button14.BackColor = Color.LimeGreen
                Button14.Text() = "Insert"
                ЦветКонтураToolStripMenuItem.Enabled = False
                ЦветToolStripMenuItem.Enabled = False
                CGToolStripMenuItem.Enabled = False
                Button1.Enabled = False
                Button2.Enabled = False
                Button3.Enabled = False
                Button4.Enabled = False
                Button10.Enabled = False
                Button11.Enabled = False
                Button20.Enabled = False
                Button27.Enabled = False
                Button28.Enabled = False
                Button29.Enabled = False
                Button30.Enabled = False
                Button31.Enabled = False
                Button33.Enabled = False
                Button34.Enabled = False
                Button35.Enabled = False
                Button36.Enabled = False

                Button38.Enabled = False
                RadioButton1.Enabled = False

            Else

                Button14.BackColor = Color.Maroon
                Button14.ForeColor = Color.White
                Button14.Text() = "Text"

                ЦветКонтураToolStripMenuItem.Enabled = True
                ЦветToolStripMenuItem.Enabled = True
                CGToolStripMenuItem.Enabled = True
                Button1.Enabled = True
                Button2.Enabled = True
                Button3.Enabled = True
                Button4.Enabled = True
                Button10.Enabled = True
                Button11.Enabled = True
                Button20.Enabled = True
                Button27.Enabled = True
                Button28.Enabled = True
                Button29.Enabled = True
                Button30.Enabled = True
                Button31.Enabled = True
                Button33.Enabled = True
                Button34.Enabled = True
                Button35.Enabled = True
                Button36.Enabled = True
                Button38.Enabled = True
                Button38.Enabled = True
                RadioButton1.Enabled = True

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' вставка поля в окно
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseClick
        Try

            If Not (Button14.BackColor = Color.LimeGreen) Then Exit Sub


            If e.Button = MouseButtons.Left Then

                With TextBox3
                    .Enabled = True
                    .Visible = True
                    .Location = New Point(e.X, e.Y)
                    .Size = New Size(300, 50)
                    .Multiline = True
                    .ForeColor = SystemColors.ControlText
                    .BackColor = SystemColors.Info
                    .BorderStyle = BorderStyle.Fixed3D
                    .ScrollBars = ScrollBars.Both
                    .Font = New Font("Times New Roman", 12.0!, FontStyle.Regular, GraphicsUnit.Point, 204)
                    .TabIndex = 0
                    .Text = " "
                End With
                Controls.Add(TextBox3)

            End If


        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' рисование текста в окне из поля. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDoubleClick

        Try

            If Not (Button14.BackColor = Color.LimeGreen) Then Exit Sub

            If e.Button = MouseButtons.Right Then
                B = True
                Dim drawFormat As New StringFormat()

                Using formGraphics As Graphics = CreateGraphics(),
                    drawFont As New Font(CType(ToolStripComboBox6.SelectedItem, String), (CType(ToolStripComboBox7.SelectedItem, Integer))),
                    drawBrush As New SolidBrush(CType(ToolStripComboBox5.SelectedItem, Color))

                    formGraphics.DrawString(TextBox3.Text(), drawFont, drawBrush,
                        e.X, e.Y, drawFormat)

                    TextBox3.Visible = False

                    TextBox3.Location = New Point(0, 0)



                End Using
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripComboBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripComboBox6.KeyPress
        Try
            ' оздание объекта и ссылка на него
            Dim ComboBx As ToolStripComboBox = CType(sender, ToolStripComboBox)

            If Asc(e.KeyChar) = Keys.Escape Then
                ' очиста текста
                ComboBx.SelectedIndex = -1
                ComboBx.Text = ""
                ContrlKey = True
            ElseIf Char.IsControl(e.KeyChar) Then
                ContrlKey = True
            Else
                ContrlKey = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ToolStripComboBox6_TextChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox6.TextChanged

        Try
            ' создание объекта и ссылка на него
            Dim ComboBx As ToolStripComboBox = CType(sender, ToolStripComboBox)

            If ComboBx.Text <> "" And Not ContrlKey Then
                ' Поиск искомого значения.
                Dim MatchText As String = ComboBx.Text
                Dim Match As Integer = ComboBx.FindString(MatchText)

                ' Если найдено, установка.
                If Match <> -1 Then
                    ComboBx.SelectedIndex = Match

                    ' выбор текста если пользователь продолжает ввод

                    ComboBx.SelectionStart = MatchText.Length
                    ComboBx.SelectionLength = ComboBx.Text.Length - ComboBx.SelectionStart
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub



    Private Sub СведенияToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles СведенияToolStripMenuItem1.Click
        Try
            Dim Frm As New Form2
            Frm.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub



    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Try
            If Button18.BackColor = Color.Gold Then

                PictureBox1.Visible = False
                Button18.BackColor = Color.Lime
                If Button18.BackColor = Color.Lime And ToolStripButton3.BackColor = Color.Green Then

                    ToolStripButton3.PerformClick()


                Else
                    PictureBox1.Visible = False
                    Button18.BackColor = Color.Lime
                End If


            Else
                PictureBox1.Visible = True
                Button18.BackColor = Color.Gold
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Try
            If Not WindowState = FormWindowState.Maximized Then Exit Sub

            If Button20.BackColor = Color.Lime = False Then Exit Sub



            FormBorderStyle = FormBorderStyle.None
            MenuStrip1.Visible = False
            ToolStrip1.Visible = False
            TabControl1.Visible = False

            Form3.FormBorderStyle = FormBorderStyle.None
            Form3.MenuStrip1.Visible = False
            Form3.ToolStrip1.Visible = False


            PictureBox1.Location = New Point(0, 90)
            PictureBox1.Size = New Size(94, 100)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' активация свертывания элемента управления
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Try
            If (ToolStripButton3.BackColor = Color.Green) = False Then
                Timer2.Enabled = True
                RemoveHandler Button13.Click, AddressOf Button13_Click
            Else

                Dim MBox As DialogResult = MessageBox.Show("Отменить перерисовку поля ( с потерей несохраненной графической информации) будет нельзя, продолжить? ", "DrawWithMouse", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                ' YES - no TextBox         NO- exit from dilog         
                If MBox = DialogResult.No Then Exit Sub
                If MBox = DialogResult.Yes Then
                    Timer2.Enabled = True
                    RemoveHandler Button13.Click, AddressOf Button13_Click

                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' свертывание элемента управления
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try
            ' движение вправо +
            TabControl1.Left = TabControl1.Left + 1 * gDirection
            ' ширина клиентской области минус ширина элемента управления + часть на величину которой бысл сделан выбор свернуть
            If TabControl1.Left >= ((ClientSize.Width - TabControl1.Width) + 110) Then
                Timer2.Enabled = False

                If ToolStripButton3.BackColor = Color.Green Then
                    ToolStripButton3.PerformClick()
                End If


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' восстановление элемента уравления в прежней позиции.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Try

            Dim MBox As DialogResult = MessageBox.Show("Часть изобразительной информации может быть утрачена, продолжить? ", "DrawWithMouse", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            ' YES - no TextBox         NO- exit from dilog         
            If MBox = DialogResult.No Then Exit Sub
            If MBox = DialogResult.Yes Then




                TabControl1.Location = New Point((ClientSize.Width - TabControl1.Width), 64)

                AddHandler Button13.Click, AddressOf Button13_Click

                AddHandler Me.Paint, AddressOf Form1_Paint



            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub








    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Refresh()
            ToolStripButton3.BackColor = Color.Red
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Try

            If Button20.Text() = "Act" Then
                Dim MBox As DialogResult = MessageBox.Show("Вернуть из БО будет нельзя, продолжить? ", "DrawWithMouse", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                ' YES - no TextBox         NO- exit from dilog         
                If MBox = DialogResult.No Then Exit Sub
                If MBox = DialogResult.Yes Then
                    Button20.BackColor = Color.Lime
                    Button20.Text() = "Save"
                End If
            Else
                Button20.BackColor = Color.Beige
                Button20.Text() = "Act"
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Form1_MouseDoubleClick1(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDoubleClick
        Try
            If Not WindowState = FormWindowState.Maximized Then PictureBox1.Visible = True

            If Not (Button20.BackColor = Color.Lime) Then Exit Sub
            If ((Button20.BackColor = Color.Lime) And (Button20.Text = "Save") And (WindowState = FormWindowState.Maximized)) Then

                If e.Button = MouseButtons.Right Then
                    B = True
                    SaveImage1()



                    ReturnALL()



                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Sub ReturnALL()
        Try
            ContrlMouse10 = True
            FormBorderStyle = FormBorderStyle.Fixed3D


            Form3.FormBorderStyle = FormBorderStyle.Fixed3D
            Form3.MenuStrip1.Visible = True
            Form3.ToolStrip1.Visible = False


            MenuStrip1.Visible = True
            ToolStrip1.Visible = True
            TabControl1.Visible = True
            PictureBox1.Visible = True
            PictureBox1.Location = New Point(5, 638)
            PictureBox1.Size = New Size(100, 100)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub

    ''' <summary>
    ''' сохранение, вставка и закрытие файла, обслуживание поля рисования
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Try

            SaveImage1()
            InsertImage()
            Button15.Enabled = False

            If originalImage IsNot Nothing Then
                originalImage.Dispose()
            Else
                Exit Sub
            End If

            If currentImage IsNot Nothing Then
                currentImage.Dispose()
            Else
                Exit Sub

            End If



            Delate()

            Close()


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub
    Sub InsertImage()
        Try
            originalImage = GetImage()
            currentImage = CType(originalImage.Clone(), Image)
            Using image1 As New Bitmap(currentImage)
                PictureBox2.Image = image1
            End Using


            BmSource = New Bitmap(PictureBox1.Image)
            BmDest = New Bitmap((BmSource.Width), (BmSource.Height))
            Corners = New Point() {New Point(0, 0), New Point(BmSource.Width, 0), New Point(0, BmSource.Height)}

            DragCorner = -1
            ' Выводим картинку. 
            WarpImage()

        Catch ex As Exception

        End Try
    End Sub


    ''' <summary>
    ''' вк,схема кода сохранения, минуя буфер обмена и окно картинок.
    ''' </summary>
    Sub SaveImage1()
        If ToolStripTextBox2.Text.Trim.Length = 0 Then Exit Sub
        If ToolStripTextBox3.Text.Trim.Length = 0 Then Exit Sub

        Try
            originalImage = GetImage()
            currentImage = CType(originalImage.Clone(), Image)
            Dim image1 As New Bitmap(currentImage)
            PictureBox2.Image = image1

            PictureBox1.Image = image1

            If (([String].IsNullOrEmpty(ToolStripTextBox2.Text) = True) Or ([String].IsNullOrEmpty(ToolStripTextBox3.Text))) Then
                MsgBox("Введите числовые параметры")
            Else
                'Объявление и декларация целочисленных переменных для выбора местоположения прорисовки картинки
                Dim a As Integer = CInt(ToolStripTextBox2.Text)
                Dim b As Integer = CInt(ToolStripTextBox3.Text)
                'Объявление и декларация переменной битмапа 
                Dim image As New Bitmap(CType(ToolStripComboBox1.SelectedItem, Integer), CType(ToolStripComboBox2.SelectedItem, Integer))
                ' Создание объекта графика, присвоение значения исполнения
                Dim графика As Graphics = Graphics.FromImage(image)
                'Объявление и декларация переменной изображения 
                Dim изображение As Image = currentImage

                ' метод прорисовки с аргументами изображения и локализации изображения (здесь, в границах битмапа)
                графика.DrawImage(изображение, x:=a, y:=b)
                'Открытие диалога сохранения существующего файла изображений в пяти форматах: bmp, gif, tiff, jpeg, png. 
                If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                    ' присвоение значения исполнения переменной изображения, извлечение картинки из буфера обмена в исполняемое значение переменной
                    ' метод прорисовки с аргументами изображения и локализации изображения (здесь, в границах битмапа)
                    графика.DrawImage(изображение, x:=a, y:=b)
                    'метод прорисовки изображения, битмапа
                    графика.DrawImage(image, 0, 0)
                    ' сохранение изображения битмапа с белым фоном и картинкой 
                    image.Save(SaveFileDialog1.FileName, GetImageFormat())
                    графика.Dispose()

                End If


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub
    ''' <summary>
    ''' функция выбора и ветки расширения файлов
    ''' </summary>
    ''' <returns></returns>
    Private Function GetImageFormat() As Imaging.ImageFormat
        'Создание функции управления выбором случая расширения файла
        Select Case SaveFileDialog1.FilterIndex
            Case 1
                Return Imaging.ImageFormat.Bmp
            Case 2
                Return Imaging.ImageFormat.Gif
            Case 3
                Return Imaging.ImageFormat.Jpeg
            Case 4
                Return Imaging.ImageFormat.Tiff
            Case Else
                Return Imaging.ImageFormat.Png
        End Select
        Return Nothing
    End Function

    ''' <summary>
    ''' функция выбора и ветки расширения файлов
    ''' </summary>
    ''' <returns></returns>
    Private Function GetImageFormat1() As Imaging.ImageFormat
        'Создание функции управления выбором случая расширения файла
        Select Case SaveFileDialog2.FilterIndex
            Case 1
                Return Imaging.ImageFormat.Bmp
            Case 2
                Return Imaging.ImageFormat.Gif
            Case 3
                Return Imaging.ImageFormat.Jpeg
            Case 4
                Return Imaging.ImageFormat.Tiff
            Case Else
                Return Imaging.ImageFormat.Png
        End Select
        Return Nothing
    End Function


    ''' <summary>
    ''' сохранение и закрытие файла
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SaveAndorRenameAndSaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAndorRenameAndSaveToolStripMenuItem.Click
        Try
            'Создание диалога сохранения существующего файла изображений в четырех форматах: bmp, gif, tiff, jpeg. 
            If SaveFileDialog2.ShowDialog() = DialogResult.OK Then
                currentImage.Save(SaveFileDialog2.FileName, GetImageFormat())

                If originalImage IsNot Nothing Then
                    originalImage.Dispose()
                Else
                    Exit Sub
                End If

                If currentImage IsNot Nothing Then
                    currentImage.Dispose()

                    Delate()
                    Close()
                Else
                    Exit Sub

                End If




            End If
        Catch Exc As FileNotFoundException

            MessageBox.Show("Нет такого файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' возврат логотипа
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ClearPBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearPBToolStripMenuItem.Click, Button23.Click
        Try
            GetObject()
            GetObject1()
        Catch ex As Exception
            MessageBox.Show("Нет такого файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Function GetObject() As Graphics
        Dim bmp As Bitmap
        bmp = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        PictureBox1.Image = bmp
        Dim G As Graphics
        G = Graphics.FromImage(bmp)
        Return G
    End Function
    Function GetObject1() As Graphics
        Dim bmp As Bitmap
        bmp = New Bitmap(PictureBox2.Width, PictureBox2.Height)
        PictureBox2.Image = bmp
        Dim G As Graphics
        G = Graphics.FromImage(bmp)
        Return G
    End Function


    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Try
            'Смена клавиатуры с русской раскладки на английскую
            'InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture( _ 
            'New CultureInfo("ru-RU")) 
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(
            New CultureInfo("en-US"))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        Try
            ' с английской на русскую
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(
        New CultureInfo("ru-RU"))
            'InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(
            'New CultureInfo("en-US"))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub BackGraundColorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Button24.Click, BackGraundColorToolStripMenuItem.Click
        Try
            ColorDialog1.Color = BackColor
            ColorDialog1.FullOpen = True
            ColorDialog1.ShowDialog()
            BackColor = ColorDialog1.Color

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Form1_ClientSizeChanged(sender As Object, e As EventArgs) Handles MyBase.ClientSizeChanged
        Try
            If (FormWindowState.Maximized = 2) And (ToolStripButton3.BackColor = Color.Green) Then

                ToolStripButton3.PerformClick()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub WarpImage()
        ' Создадим объект Graphics для картинки. 
        Dim gr_dest As Graphics = Graphics.FromImage(BmDest)
        ' Копируем картинку-источник в целевую картинку, 
        gr_dest.Clear(PictureBox2.BackColor)
        gr_dest.DrawImage(BmSource, Corners)
        ' Рисуем три метки-окружности. 
        Dim i As Long
        For i = 0 To 2
            Dim pn As New Pen(Color.Red, 3)
            gr_dest.DrawEllipse(pn,
            Corners(CInt(i)).X - CORNER_RADIUS,
            Corners(CInt(i)).Y - CORNER_RADIUS,
            2 * CORNER_RADIUS, 2 * CORNER_RADIUS)
        Next i


        ' Показываем результат. 
        PictureBox2.Image = BmDest
    End Sub


    Private Sub PictureBox2_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseDown
        Try
            Dim i As Long
            ' Если мышь рядом с одной из меток. 
            For i = 0 To 2
                If (Math.Abs(Corners(CInt(i)).X - e.X) < CORNER_RADIUS) And
    (Math.Abs(Corners(CInt(i)).Y - e.Y) < CORNER_RADIUS) Then
                    ' Начинаем перетаскивать угол. 
                    DragCorner = i
                    Exit For
                End If
            Next i
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub PictureBox2_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseMove
        Try
            ' Проверка, перетаскиваем ли угол картинки. 
            If DragCorner < 0 Then Exit Sub
            Corners(CInt(DragCorner)).X = e.X
            If Corners(CInt(DragCorner)).X < 0 Then
                Corners(CInt(DragCorner)).X = 0
            ElseIf Corners(CInt(DragCorner)).X > BmDest.Width Then
                Corners(CInt(DragCorner)).X = BmDest.Width
            End If
            Corners(CInt(DragCorner)).Y = e.Y
            If Corners(CInt(DragCorner)).Y < 0 Then
                Corners(CInt(DragCorner)).Y = 0
            ElseIf Corners(CInt(DragCorner)).Y > BmDest.Height Then
                Corners(CInt(DragCorner)).Y = BmDest.Height
            End If
            WarpImage()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub PictureBox2_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseUp
        Try
            DragCorner = -1

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub PictureBox2_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles PictureBox2.MouseDoubleClick
        Try
            If e.Button = MouseButtons.Right Then
                ContrlMouse2 = True

                SaveWImage()


            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Sub SaveWImage()
        Try
            'Save Image
            If SaveFileDialog3.ShowDialog() = DialogResult.OK Then
                PictureBox2.Image.Save(SaveFileDialog3.FileName, GetImageFormat2())

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub SaveFromScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveFromScreenToolStripMenuItem.Click

        Try

            Dim memoryImage As Bitmap


            Dim myGraphics As Graphics = CreateGraphics()
            Dim s As Size = Size
            memoryImage = New Bitmap(s.Width, s.Height, myGraphics)
            Dim memoryGraphics As Graphics = Graphics.FromImage(memoryImage)
            memoryGraphics.CopyFromScreen(CInt(ToolStripTextBox13.Text()), CInt(ToolStripTextBox14.Text()), CInt(ToolStripTextBox15.Text()), CInt(ToolStripTextBox16.Text()), s)


            If SaveFileDialog2.ShowDialog() = DialogResult.OK Then

                ' сохранение изображения битмапа с белым фоном и картинкой 
                memoryImage.Save(SaveFileDialog2.FileName, GetImageFormat2())
                memoryImage.Dispose()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        Try

            If (SelectionArea.Rectangle.Width = 0 And SelectionArea.Rectangle.Height = 0) Then Exit Sub

            Dim memoryImage As Bitmap


            Dim myGraphics As Graphics = CreateGraphics()
            Dim s As New Size(SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height)
            memoryImage = New Bitmap(s.Width, s.Height, myGraphics)
            Dim memoryGraphics As Graphics = Graphics.FromImage(memoryImage)
            memoryGraphics.CopyFromScreen(SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y, 0, 0, s)


            If SaveFileDialog2.ShowDialog() = DialogResult.OK Then

                ' сохранение изображения битмапа с белым фоном и картинкой 
                memoryImage.Save(SaveFileDialog2.FileName, GetImageFormat2())
                memoryImage.Dispose()


            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click, CopyToCBFromScreenToolStripMenuItem.Click
        Try
            If RadioButton2.Checked = True Then Exit Sub
            If (SelectionArea.Rectangle.Width = 0 And SelectionArea.Rectangle.Height = 0) Then Exit Sub

            Dim memoryImage As Bitmap


            Dim myGraphics As Graphics = CreateGraphics()
            Dim s As New Size(SelectionArea.Rectangle.Width, SelectionArea.Rectangle.Height)
            memoryImage = New Bitmap(s.Width, s.Height, myGraphics)

            Dim memoryGraphics As Graphics = Graphics.FromImage(memoryImage)
            memoryGraphics.CopyFromScreen(SelectionArea.Rectangle.X, SelectionArea.Rectangle.Y, 0, 0, s)

            originalImage = memoryImage
            currentImage = CType(originalImage.Clone(), Image)
            Dim image1 As New Bitmap(currentImage)
            PictureBox1.Image = image1

            My.Computer.Clipboard.SetImage(PictureBox1.Image)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub


    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        Try
            ЦветКонтураToolStripMenuItem.BackColor = Color.Red
            ЦветToolStripMenuItem.BackColor = Color.Red
            CGToolStripMenuItem.BackColor = Color.Red
            Button1.BackColor = Color.Red
            Button2.BackColor = Color.Red
            Button3.BackColor = Color.Red
            Button4.BackColor = Color.Red
            Button10.BackColor = Color.Red
            Button11.BackColor = Color.Red
            Button27.BackColor = Color.Red
            Button28.BackColor = Color.Red
            Button29.BackColor = Color.Red
            Button30.BackColor = Color.Red
            Button31.BackColor = Color.Red
            Button33.BackColor = Color.Red
            Button34.BackColor = Color.Red
            Button35.BackColor = Color.Red
            Button36.BackColor = Color.Red


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    'вставка из буфера обмена, в клик, опция полезна в 10-ке последних версий. 
    Private Sub Me_MouseDown100(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Try

            If RadioButton1.Checked = True Then Exit Sub

            If Not RadioButton3.Checked Then Exit Sub

            sel = True

            Dim screenPoint As Point
            screenPoint = PointToScreen(New Point(e.X, e.Y))


            If My.Computer.Clipboard.ContainsImage Then
                originalImage = CType(My.Computer.Clipboard.GetImage, Bitmap)
                currentImage = CType(originalImage.Clone(), Image)
                Dim Графика As Graphics = CreateGraphics()
                Графика.DrawImage(originalImage, screenPoint)
                Графика.Dispose()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub ClearCBToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ClearCBToolStripMenuItem1.Click, Button22.Click
        Try
            'Clear the clipboard
            My.Computer.Clipboard.Clear()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub



    Private Function GetImageFormat2() As Imaging.ImageFormat
        'Создание функции управления выбором случая расширения файла
        Select Case SaveFileDialog3.FilterIndex
            Case 1
                Return Imaging.ImageFormat.Bmp
            Case 2
                Return Imaging.ImageFormat.Gif
            Case 3
                Return Imaging.ImageFormat.Jpeg
            Case 4
                Return Imaging.ImageFormat.Tiff
            Case Else
                Return Imaging.ImageFormat.Png
        End Select
        Return Nothing
    End Function

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click

        If Button38.Text() = "Accb" Then
            Button38.BackColor = Color.Lime
            Button38.Text() = "Boad"
            RadioButton1.Enabled = False

        Else
            Button38.BackColor = Color.Beige
            Button38.Text() = "Accb"
            RadioButton1.Enabled = True

        End If

    End Sub

    Private Sub PastFromCBToScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PastFromCBToScreenToolStripMenuItem.Click, Button16.Click
        Try


            If My.Computer.Clipboard.ContainsImage Then
                Dim изображение As Image
                изображение = My.Computer.Clipboard.GetImage()
                Dim Графика As Graphics = CreateGraphics()
                Графика.DrawImage(изображение, x:=0, y:=-75)
                Графика.Dispose()

            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox5.KeyPress, TextBox4.KeyPress, TextBox2.KeyPress, ToolStripTextBox7.KeyPress, ToolStripTextBox10.KeyPress
        Try
            ' Вхождение десятичных знаков и Backspace:
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return
            If e.KeyChar = Convert.ToChar(Keys.Back) Then Return ' или Exit Sub
            ' Ограничение на ввод иных символических знаков:
            e.Handled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub



    ' вставка изображенния из клинской области рисования в область трансформации. 
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click

        Try
            originalImage = GetImage()
            currentImage = CType(originalImage.Clone(), Image)
            Dim image1 As New Bitmap(currentImage)
            PictureBox2.Image = image1


            BmSource = New Bitmap(image1)
            BmDest = New Bitmap((BmSource.Width), (BmSource.Height))
            Corners = New Point() {New Point(0, 0), New Point(BmSource.Width, 0), New Point(0, BmSource.Height)}

            DragCorner = -1
            ' Выводим картинку. 
            WarpImage()


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub
    ' обслуживание буфера обмена
    Private Sub ClearCBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearCBToolStripMenuItem.Click

        Try

            If My.Computer.Clipboard.ContainsImage() Then

                'Clear the clipboard
                My.Computer.Clipboard.Clear()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub



    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Try
            Try
                TabControl1.Location = New Point(200, 49)
                TabControl1.Size = New Size(1100, 676)
                AddHandler Button13.Click, AddressOf Button13_Click

                Refresh()
                Delate()
                ToolStripButton3.BackColor = Color.Red

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub



    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        Try
            TabControl1.Size = New Size(138, 676)
            TabControl1.Location = New Point((ClientSize.Width - TabControl1.Width), 49)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub
    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then TextBox4.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If TextBox4.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then TextBox5.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If TextBox5.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub


    Private Sub ToolStripTextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox1.KeyPress

        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox1.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox1.Text() = Nothing Then Exit Sub

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try




    End Sub
    Private Sub ToolStripTextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox2.KeyPress

        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox2.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox2.Text() = Nothing Then Exit Sub

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub

    Private Sub ToolStripTextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox3.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox3.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox3.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub
    Private Sub ToolStripTextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox4.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox4.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox4.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub ToolStripTextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox5.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox5.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox5.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub


    Private Sub ToolStripTextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox6.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox6.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox6.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub ToolStripTextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox7.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox7.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox7.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub ToolStripTextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox9.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox9.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox9.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub ToolStripTextBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox10.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox10.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox10.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub ToolStripTextBox11_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox11.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox11.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox11.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub ToolStripTextBox12_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox12.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox11.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox12.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub ToolStripTextBox13_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox13.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox13.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox13.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub ToolStripTextBox14_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox14.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox14.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox14.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub


    Private Sub ToolStripTextBox15_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox15.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox15.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox15.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub ToolStripTextBox16_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToolStripTextBox16.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) = True Then Exit Sub ' или Return

            If e.KeyChar = Convert.ToChar(Keys.Back) Then ToolStripTextBox16.Text() = CStr(0)

            ' Запрет на ввод других вводимых символов:
            e.Handled = True

            If ToolStripTextBox16.Text() = Nothing Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub


    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Try
            HScrollBar1.Enabled = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click

        Try
            If Button32.BackColor = Color.Crimson Then
                HScrollBar1.Enabled = False
                Button32.BackColor = Color.Green
            Else
                HScrollBar1.Enabled = True
                Button32.BackColor = Color.Crimson
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub
    ''' <summary>
    ''' Закрытие окна, графического файла, очистка клиентской области.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DelateToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DelateToolStripMenuItem1.Click
        Try
            ToolStripButton3.BackColor = Color.Red

            Refresh()
            Delate()
            Close()

            If originalImage IsNot Nothing Then
                originalImage.Dispose()
                Close()
            Else
                Exit Sub
            End If

            If currentImage IsNot Nothing Then
                currentImage.Dispose()
                Close()
            Else
                Exit Sub

            End If





        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try
    End Sub



    ' копирование фрагментов прорисовки после каждого подъема и фиксации кнопки мыши.
    Private Sub Form1_MouseUpZ7777(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        Try
            If Button38.BackColor = Color.Lime Then
                Dim Picture As Bitmap = GetImage()


                My.Computer.Clipboard.SetImage(Picture)
            Else Exit Sub

            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try


    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        'Изображение разноцветных квадратов на бланке формы.
        Dim canvas As Graphics = e.Graphics
        canvas.FillRectangle(New SolidBrush(Color.White), 1000, 35, 500, 700)
    End Sub

    ''' <summary>
    ''' закрытие окна и графического файла.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            If originalImage IsNot Nothing Then
                originalImage.Dispose()
            Else
                Exit Sub
            End If

            If currentImage IsNot Nothing Then
                currentImage.Dispose()
            Else
                Exit Sub

            End If

            Delate()

        Catch ex As Exception

        End Try
    End Sub



#End Region






End Class
