using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManuallyThrowingCustomException
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        BindingSource showProductList;

        public class NumberFormatException : Exception
        {
            public NumberFormatException(string nmr) : base(nmr) { }
        }

        public class StringFormatException : Exception
        {
            public StringFormatException(string str) : base(str) { }
        }

        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string crncy) : base(crncy) { }
        }
        public frmAddProduct()
        {
            InitializeComponent();

            showProductList = new BindingSource();
        }
        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            
            string[] ListOfProductCategory = new string[]
            {
                "Beverages", "Bread/Bakery", "Canned/Jarred Goods", "Dairy", "Frozen", "Meat", "Personal Care", "Other"

            };
            foreach (string prdctCategory in ListOfProductCategory)
            {
                cbCategory.Items.Add(prdctCategory);
            }

        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellPrice = SellingPrice(txtSellPrice.Text);
            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
            _ExpDate, _SellPrice, _Quantity, _Description));
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;

            
        }
        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                    //Exception here
                    throw new StringFormatException("Letters Only");
            }
            catch (StringFormatException se)
            {
                MessageBox.Show(se.Message);
            }
                    return name;

        }
        public int Quantity(string qty)
        {
            try
            {
                if (!Regex.IsMatch(qty, @"^[0-9]"))
                    //Exception here             
                    throw new NumberFormatException("Numbers Only");
            }
            catch (NumberFormatException ne)
            {
                MessageBox.Show(ne.Message);
            }
                    return Convert.ToInt32(qty);

        }
        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                    //Exception here
                    throw new CurrencyFormatException("Invalid Currency");
            }
            catch (CurrencyFormatException ce)
            {
                MessageBox.Show(ce.Message);

            }
                    return Convert.ToDouble(price);
        }



    }

}
