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
            btnDeletar.Visible = true;
            btnUpdate.Visible = true;
            //variáveis para usar nos dentro dos códigos
        }

        string continua = "yes";
        private void label3_Click(object sender, EventArgs e)
        {


        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            verificaVazio();
            //Void criado para ser utilizado dentro dos botoes,responsável por verificar se a caixa de texto está vazia ou não

            if (continua == "yes")
            {
                try
                {
                    using (MySqlConnection cnn = new MySqlConnection())
                    {
                        cnn.ConnectionString = "server=localhost;database=contatos;uid=root;pwd=;port=3306";
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

            //variáveis criadas de acordo com os voids,tem função de mostrar o que foi escrito nos campos e atualiza(limpa) o campo para um novo texto
            mostrar();
            limpar();
        }
        void mostrar()
        {
            //void responsável por mostrar  os caracteres nos campos 
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
            //void responsável por limpar os campos após algum dado ser inserido,e possui opçoes para o botão ser visível ou não
        void limpar()
        {
            txtId.Text = "";
            txtNome.Clear();
            txtEmail.Text = "";
            btnInserir.Text = "INSERIR";
            btnDeletar.Visible = true;
            btnUpdate.Visible = true;
        }

           //void responsávelpor verificar se o campo está vazio não,caso esteja vazio,mostrará uma mensagem de aviso
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

            //void responsável por mostrar os dados inseridos dentro da tabela
        private void dgwTabela_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgwTabela.CurrentRow.Index != -1) 
            {
                txtId.Text = dgwTabela.CurrentRow.Cells[0].Value.ToString();
                txtNome.Text = dgwTabela.CurrentRow.Cells[1].Value.ToString();
                txtEmail.Text = dgwTabela.CurrentRow.Cells[2].Value.ToString();
                btnDeletar.Visible = true;
                btnUpdate.Visible = true;
                btnInserir.Text = "Novo";


            }
        }
            //dentro do botão deletar,temos opçoes para deletar os dados e opção para confirmar ,limpa os campos
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
            //botão para atualizar as caixas,faz uma alteração nos campos e possui uma opção para confirmar quando a atualização for feita
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
            //botão para pesquisar algum dado dentro da tabela,podemos pesquisar por nome ou email selecionando os botoes com as opçoes
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection cnn = new MySqlConnection())
                {
                    cnn.ConnectionString = "server=localhost;database=contatos;uid=root;pwd=;port=3306";
                    cnn.Open();
                    string sql;


            //comando if para alterar se a pesquisa vai ser feita por email ou contato(nome)
                    if (rbEmail.Checked)
                    {
                        sql = "Select * from contatos where email Like'" + txtPesquisar.Text + "%'";
                    }
                    else
                    {
                        sql = "Select * from contatos where nome Like'" + txtPesquisar.Text + "%'";
                    }





            //permite verificar os dados salvos dentro da tabela
                    MySqlCommand cmd = new MySqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
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
    }
}
