using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System_do_zarządzania_magazynem.Models;

namespace System_do_zarządzania_magazynem
{
    public partial class AddProductForm : Form
    {
        public bool IsEditMode { get; set; } = false;
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public Decimal price { get; set; }
        public AddProductForm()
        {
            InitializeComponent();
            
        }
        private void AddProductForm_Load(object sender, EventArgs e)
        {
            if (!IsEditMode)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";

                button2.Text = "Dodaj";
                button2.Click += Add;
                button2.Click -= Edit;
            }
            else
            {
                button2.Text = "Edytuj";
                button2.Click -= Add;
                button2.Click += Edit;
            }
             
        }

        private void Add(object sender, EventArgs e)
        {
            ProductName = textBox1.Text;
            int Quantitytemp;
            string input = textBox3.Text.Replace('.', ',');
            string input2 = textBox2.Text;
            decimal tempPrice;
            if (!decimal.TryParse(input,out tempPrice))
            {
                MessageBox.Show("Cena musi być liczbą!");
                return;
            }else if(!int.TryParse(input2, out Quantitytemp)){
                MessageBox.Show("ilość musi być liczbą!");
                return;
            }
            if (textBox1.Text == "" || Quantitytemp < 0 || tempPrice <= 0)
            {
                MessageBox.Show("Upewnij się że wszytko jest poprawnie uzupełnione");
                return;
            }
            price = tempPrice;
            Quantity = Quantitytemp;

            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        public void SetProduct(Product product)
        {
            textBox1.Text = product.ProductName;
            textBox2.Text = product.Quantity.ToString();
            textBox3.Text = product.Price.ToString();

            IsEditMode = true;
        }

        private void Edit(object sender, EventArgs e)
        {
            ProductName = textBox1.Text;

            int Quantitytemp;
            string input = textBox3.Text.Replace('.', ',');
            string input2 = textBox2.Text;
            decimal tempPrice;
            if (!decimal.TryParse(input, out tempPrice))
            {
                MessageBox.Show("Cena musi być liczbą!");
                return;
            }
            else if (!int.TryParse(input2, out Quantitytemp))
            {
                MessageBox.Show("ilość musi być liczbą!");
                return;
            }
            if (Name == "" || Quantitytemp < 0 || tempPrice <= 0)
            {
                MessageBox.Show("Upewnij się że wszytko jest poprawnie uzupełnione");
                return;
            }
            price = tempPrice;
            Quantity = Quantitytemp;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void Anuluj(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
    }
}
