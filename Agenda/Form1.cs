using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mostrar();
            btnDeletar.Visible = false;
            btnInserir.Visible = false;

        }

        string continua = "yes";
        private void label3_Click(object sender, EventArgs e)
        {


        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            verificaVazio();


            if (continua == "yes")
            {
                try
                {
                    using (MySqlConnection cnn = new MySqlConnection())
                    {
                        cnn.ConnectionString = "server=localhost;database=agenda;uid=root;pwd=;port=3306";
                        cnn.Open();
                        MessageBox.Show("Inserido com sucesso!");
                        string sql = "insert into contatos (nome, email) values ('" + txtNome.Text + "', '" + txtEmail.Text + "')";
                        MySqlCommand cmd = new MySqlCommand(sql, cnn);
                        cmd.ExecuteNonQuery();



                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }


            mostrar();
            limpar();
        }

        void mostrar()
        {

            try
            {
                using (MySqlConnection cnn = new MySqlConnection())
                {
                    cnn.ConnectionString = "server=localhost;database=contatos;uid=root;pwd=;port=3306";
                    cnn.Open();
                    string sql = "Select * from contatos";
                    DataTable table = new DataTable();
                    MySqlDataAdapter adpter = new MySqlDataAdapter(sql, cnn);
                    adpter.Fill(table);
                    dgwTabela.DataSource = table;

                    dgwTabela.AutoGenerateColumns = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            

        }
        void limpar()
        {
            txtId.Text = "";
            txtNome.Clear();
            txtEmail.Text = "";
        }


        void verificaVazio()
        {
            if (txtNome.Text == "" || txtEmail.Text == "")
            {
                continua = "no";
                MessageBox.Show("Preencha todos os campos");
            }
            else
            {
                continua = "yes";
            }
        }


        private void dgwTabela_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgwTabela.CurrentRow.Index != -1) 
            {
                txtId.Text = dgwTabela.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = dgwTabela.CurrentRow.Cells[1].Value.ToString();
                txtEmail.Text = dgwTabela.CurrentRow.Cells[2].Value.ToString();
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Deseja realmente excluir", "Confirmação", MessageBoxButtons.YesNo))
            {

                try
                {
                    using (MySqlConnection cnn = new MySqlConnection())
                    {
                        cnn.ConnectionString = "server=localhost;database=contatos;uid=root;pwd=;port=3306";
                        cnn.Open();
                        string sql = "Delete from contatos where idContato = '" + txtId.Text + "'";
                        MySqlCommand cmd = new MySqlCommand(sql, cnn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(" Deletado com sucesso! ");

                    }
                    limpar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
            mostrar();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection())
                {
                    cnn.ConnectionString = "server=localhost;database=contatos;uid=root;pwd=;port=3306";
                    cnn.Open();
                    string sql = "Update contatos set nome='" + txtNome.Text + "', email='" + txtEmail.Text + "' where idContato='" + txtId.Text + "'";
                    MySqlCommand cmd = new MySqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Atualizado com sucesso!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            mostrar();
        }
    }
}
