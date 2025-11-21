Imports MySql.Data.MySqlClient

Public Class Form1

    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand

    Private Sub ButtonConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click
        conn = New MySqlConnection
        conn.ConnectionString = "server = localhost; user id = root; password = root; database = crud_demo_db_santillan"


        Try
            conn.Open()
            MessageBox.Show("Connection Successful")
            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            conn.Close()
        End Try



    End Sub

    Private Sub ButtonCreate_Click(sender As Object, e As EventArgs) Handles ButtonCreate.Click
        Dim query As String = "INSERT INTO `crud_demo_db_santillan`.`students_tbl` ( `Name`, `age`, `email`) VALUES ( @Name, @age, @email);"
        Try
            Using conn As New MySqlConnection("server = localhost; user id = root; password = root; database = crud_demo_db_santillan")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", CInt(TextBoxAge.Text))
                    cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record Inserted Successfully")
                    TextBoxName.Clear()
                    TextBoxAge.Clear()
                    TextBoxEmail.Clear()

                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub buttonRead_Click(sender As Object, e As EventArgs) Handles buttonRead.Click
        Dim query As String = "SELECT * FROM crud_demo_db_santillan.students_tbl;"
        Try
            Using conn As New MySqlConnection("server = localhost; user id = root; password = root; database = crud_demo_db_santillan")
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridView.DataSource = table
                DataGridView.Columns("id").Visible = False

            End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView.CellContentDoubleClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView.Rows(e.RowIndex)
            TextBoxName.Text = selectedRow.Cells("Name").Value.ToString()
            TextBoxAge.Text = selectedRow.Cells("age").Value.ToString()
            TextBoxEmail.Text = selectedRow.Cells("email").Value.ToString()
            TextBoxHiddenId.Text = selectedRow.Cells("id").Value.ToString()


        End If
    End Sub


End Class
