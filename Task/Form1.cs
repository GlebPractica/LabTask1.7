using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task
{
    public partial class Form1 : Form
    {
        private ClientManager clientManager;

        private bool _name = false;
        private bool _surname = false;
        private bool _numacc = false;
        private bool _sumacc = false;
        private bool _search = false;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            clientManager = new ClientManager();
            textBox1.Validating += Name_Validating;
            textBox2.Validating += Surname_Validating;
            textBox3.Validating += NumberAccount_Validating;
            textBox4.Validating += SumAccount_Validating;
            textBox6.Validating += Search_Validating;
        }

        private void Search_Validating(object sender, CancelEventArgs e)
        {
            if (!int.TryParse(textBox6.Text, out int N))
            {
                errorProvider5.SetError(textBox6, "Неверно введен номер");
                _search = false;
                return;
            }

            _search = true;
            errorProvider5.Clear();
        }

        private void SumAccount_Validating(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(textBox4.Text, out float N))
            {
                errorProvider4.SetError(textBox4, "Неверно введено число.");
                _sumacc = false;
                return;
            }

            _sumacc = true;
            errorProvider4.Clear();
        }

        private void NumberAccount_Validating(object sender, CancelEventArgs e)
        {
            if (!int.TryParse(textBox3.Text, out int N) && N < 1)
            {
                errorProvider3.SetError(textBox3, "Неверно введено число. Оно должно быть целым и > 0");
                _numacc = false;
                return;
            }

            _numacc = true;
            errorProvider3.Clear();
        }

        private void Surname_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider2.SetError(textBox2, "Вы ничего не ввели");
                _surname = false;
                return;
            }

            _surname = true;
            errorProvider2.Clear();
        }

        private void Name_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Вы ничего не ввели");
                _name = false;
                return;
            }

            _name = true;
            errorProvider1.Clear();
        }

        private void BttnAdd_Click(object sender, EventArgs e)
        {
            if (_name && _surname && _numacc && _sumacc)
            {
                clientManager.AddClient(new Clients
                {
                    Name = textBox1.Text,
                    SurName = textBox2.Text,
                    TypeAccount = (comboBox1.Text == "Срочный") ? TypesAccount.Term : TypesAccount.Preferential,
                    NumberAccount = int.Parse(textBox3.Text),
                    SumAccount = float.Parse(textBox4.Text),
                    DateOfLastChange = dateTimePicker1.Value
                });
                UpdateGridView();

                _name = false;
                _surname = false;
                _numacc = false;
                _sumacc = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void BttnRemoveSet_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                if (selectedRow.DataBoundItem is Clients client)
                {
                    clientManager.RemoveClient(client.NumberAccount);
                    UpdateGridView();
                    MessageBox.Show("Клиент с номером аккаунта " + client.NumberAccount + " был удален");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента для удаления");
            }
        }

        private void UpdateGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = clientManager.GetAllClients().ToList();
        }

        private void BttnRemoveAll_Click(object sender, EventArgs e)
        {
            clientManager.RemoveAllClients();
        }

        private void BttnUpdate_Click(object sender, EventArgs e)
        {
            UpdateGridView();
        }

        private void BttnSearch_Click(object sender, EventArgs e)
        {
            if (_search)
            {
                int searchNum = int.Parse(textBox6.Text);

                var result = clientManager.SearchByAccountNumber(searchNum).ToList();

                if (result.Any())
                {
                    MessageBox.Show($"Было найдено {result.Count} записей", "Результат");
                    dataGridView1.DataSource = result;
                }
                else
                {
                    MessageBox.Show("Ничего не было найдено", "Результат");
                    UpdateGridView();
                }
            }
        }
    }
}
