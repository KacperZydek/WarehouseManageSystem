using System.Text.Json;
using System_do_zarządzania_magazynem.Configuration;
using System_do_zarządzania_magazynem.Data;
using System_do_zarządzania_magazynem.Models;

namespace System_do_zarządzania_magazynem
{
    public partial class Form1 : Form
    {
        public List<Product> Products = new List<Product>();
        ProductRepository repo = new ProductRepository();
        public Form1()
        {
            InitializeComponent();
        }
        private AddProductForm addProductForm = new AddProductForm();

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Add(object sender, EventArgs e)
        {
            if (addProductForm.ShowDialog() == DialogResult.OK)
            {
                Product product = new Product(
                    addProductForm.ProductName,
                    addProductForm.Quantity,
                    addProductForm.price
                    );
                repo.AddProduct(product);
                int difference = product.Quantity;
                LogAction("Dodano", product, difference);
                LoadData();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var product = (Product)dataGridView1.Rows[e.RowIndex].DataBoundItem;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                var result = MessageBox.Show(
                    "Czy na pewno usunąć?",
                    "Potwierdzenie",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );

                if (result == DialogResult.Yes)
                {
                    int difference = -product.Quantity;
                    repo.DeleteProduct(product);
                    LogAction("Usunięto", product, difference);
                    LoadData();
                }
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
            {
                product = (Product)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                AddProductForm form = new AddProductForm();
                form.SetProduct(product);
                int oldQuantity = product.Quantity;
                if (form.ShowDialog() == DialogResult.OK)
                {

                    product.ProductName = form.ProductName;
                    product.Quantity = form.Quantity;
                    product.Price = form.price;


                    product.Quantity = form.Quantity;
                    int difference = product.Quantity - oldQuantity;

                    repo.EditProduct(product);
                    LogAction("Edytowano", product, difference);
                    LoadData();
                }
            }
        }

        private void LoadData()
        {
            Products = repo.GetProducts();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Products;

            PaintRows();
            AddButtons();
            AddLpColumn();
            AddRowNumbers();
            SetupDataGridView();
        }
        private void AddLpColumn()
        {
            if (!dataGridView1.Columns.Contains("Lp"))
            {
                dataGridView1.Columns.Insert(0, new DataGridViewTextBoxColumn()
                {
                    Name = "Lp",
                    HeaderText = "Lp",
                    ReadOnly = true
                });
            }
        }
        private void AddRowNumbers()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["Lp"].Value = i + 1;
            }
        }
        private void PaintRows()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Quantity"] != null)
                {
                    DataGridViewCell cell = row.Cells["Quantity"];
                    int Quantity = (int)row.Cells["Quantity"].Value;
                    if (Quantity <= 0)
                    {
                        cell.Style.BackColor = Color.LightCoral;
                    }
                    else if (Quantity > 0 && Quantity < 10)
                    {
                        cell.Style.BackColor = Color.LightYellow;
                    }
                    else
                    {
                        cell.Style.BackColor = Color.LightGreen;
                    }
                }
            }
        }
        private void AddButtons()
        {
            if (!dataGridView1.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn editBtn = new DataGridViewButtonColumn();
                editBtn.Name = "Edit";
                editBtn.HeaderText = "Edytuj";
                editBtn.Text = "Edytuj";
                editBtn.UseColumnTextForButtonValue = true;

                dataGridView1.Columns.Add(editBtn);
            }

            if (!dataGridView1.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
                deleteBtn.Name = "Delete";
                deleteBtn.HeaderText = "Usuń";
                deleteBtn.Text = "Usuń";
                deleteBtn.UseColumnTextForButtonValue = true;

                dataGridView1.Columns.Add(deleteBtn);

            }
        }
        private void LogAction(string action, Product product, int difference)
        {
            string path = "log.json";
            List<Log> logs;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    logs = JsonSerializer.Deserialize<List<Log>>(json) ?? new List<Log>();
                }
                else
                {
                    logs = new List<Log>();
                }
            }
            else
            {
                logs = new List<Log>();
            }
            logs.Add(new Log
            {
                Action = action,
                ID = product.ID,
                Name = product.ProductName,
                QuantityChange = difference,
                Price = product.Price,
                Date = DateTime.Now
            });
            string newJson = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, newJson);

        }

        private void Search(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                
                LoadData();
                return;
            }

            var filtered = Products
                .Where(p => p.ProductName
                    .Contains(textBox1.Text, StringComparison.OrdinalIgnoreCase))
                .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = filtered;
            SetupDataGridView();
            PaintRows();
        }
        private void SetupDataGridView()
        {
            dataGridView1.Columns["ID"].Visible = false;

            dataGridView1.Columns["Lp"].DisplayIndex = 0;
            dataGridView1.Columns["ProductName"].DisplayIndex = 1;
            dataGridView1.Columns["Quantity"].DisplayIndex = 2;
            dataGridView1.Columns["Price"].DisplayIndex = 3;
            dataGridView1.Columns["Edit"].DisplayIndex = 4;
            dataGridView1.Columns["Delete"].DisplayIndex = 5;

            dataGridView1.Columns["ProductName"].HeaderText = "Nazwa";
            dataGridView1.Columns["Quantity"].HeaderText = "Ilość";
            dataGridView1.Columns["Price"].HeaderText = "Cena";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
