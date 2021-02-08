using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Library
{
    public partial class Form1 : Form
    {
    //    explicit 
      //      implicit
        public BinaryFormatter binary = new BinaryFormatter();
        public Form1()
        {
            InitializeComponent();
            archive = new List<Book>();
        }

        [Serializable]
        //может быть обработка чего-либо и до класса и после
        public class Book : IComparable<Book>
        {
            public string isbn;
            public string author;
            public string name;
            public string publisher;
            public string year;
            public string price;
            public Book()
            { }
            public Book(string isbn, string author, string name, string publisher, string year, string price)
            {
                this.isbn = isbn;
                this.author = author;
                this.name = name;
                this.publisher = publisher;
                this.year = year;
                this.price = price;
            }
            public override bool Equals(object obj)
            {
                Book book = obj as Book;
                if (this.isbn == book.isbn && this.author == book.author && this.name == book.name
                    && this.publisher == book.publisher && this.price == book.price)
                    return true;
                else
                    return false;
            }
            new public virtual string ToString()
            {
                string[] str = new string[] {this.isbn, this.author, this.name, this.publisher, this.year, this.price};
                return String.Join(" ", str);
            }
            public override int GetHashCode()
            {
                return base.GetHashCode().GetHashCode();
            }
            public int CompareTo(Book book)
            {
                return this.price.CompareTo(book.price);
            }
        }

        public List<Book> archive;
        public Dictionary<string, Book> sortSystem = new Dictionary<string, Book>();
        public bool sortStyle = true;

        private void loadBtn_Click(object sender, EventArgs e)
        {
            dataGrid.Rows.Clear();
            archive.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.Cancel)
            {
                string str = dialog.FileName;
                try
                {
                    FileStream filestream = new FileStream(str, FileMode.OpenOrCreate);
                    
                        archive = (List<Book>)binary.Deserialize(filestream);
                    
                    int amount = archive.Count;
                    dataGrid.Rows.Add(amount);
                    int i = 0;
                    foreach (Book book in archive)
                    {
                        if (i < amount)
                        {
                            dataGrid.Rows[i].Cells[0].Value = book.isbn;
                            dataGrid.Rows[i].Cells[1].Value = book.author;
                            dataGrid.Rows[i].Cells[2].Value = book.name;
                            dataGrid.Rows[i].Cells[3].Value = book.publisher;
                            dataGrid.Rows[i].Cells[4].Value = book.year;
                            dataGrid.Rows[i].Cells[5].Value = book.price;
                            i++;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Incorrect file format");
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            bool isbnChk = false;
            var counter = dataGrid.Rows.Count;
            archive.Clear();
            for (int i = 0; i < counter - 1; i++)
            {
                Book book = new Book("","","","","","");
                Book temp = new Book();
                temp.author = "tdytg";
                Switcher(dataGrid.Rows[i].Cells[0], ref book.isbn);
                Switcher(dataGrid.Rows[i].Cells[1], ref book.author);
                Switcher(dataGrid.Rows[i].Cells[2], ref book.name);
                Switcher(dataGrid.Rows[i].Cells[3], ref book.publisher);
                Switcher(dataGrid.Rows[i].Cells[4], ref book.year);
                Switcher(dataGrid.Rows[i].Cells[5], ref book.price);
                archive.Add(book);
                if (i != counter - 1)
                    isbnChk = isbnChecker(dataGrid.Rows[i].Cells[0].Value.ToString());
            }
            if (isbnChk == true)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (dialog.ShowDialog() != DialogResult.Cancel)
                {
                    string str = dialog.FileName;
                    try
                    {
                        using (FileStream filestream = new FileStream(str, FileMode.OpenOrCreate))
                        {
                            binary.Serialize(filestream, archive);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Incorrect file format");
                    }
                }
            }
            else
            {
                MessageBox.Show("Incorrect ISBN");
            }
        }

        public void Switcher(DataGridViewCell cell, ref string val)
        {
            if (cell.Value == null)
                val = "";
            else
                val = cell.Value.ToString();
        }

        private void dataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool isbnChk = false;
            var counter = dataGrid.Rows.Count;
            archive.Clear();
            for (int ind = 0; ind < counter - 1; ind++)
            {
                Book book = new Book("", "", "", "", "", "");
                Switcher(dataGrid.Rows[ind].Cells[0], ref book.isbn);
                Switcher(dataGrid.Rows[ind].Cells[1], ref book.author);
                Switcher(dataGrid.Rows[ind].Cells[2], ref book.name);
                Switcher(dataGrid.Rows[ind].Cells[3], ref book.publisher);
                Switcher(dataGrid.Rows[ind].Cells[4], ref book.year);
                Switcher(dataGrid.Rows[ind].Cells[5], ref book.price);
                archive.Add(book);
                if (ind != counter - 1)
                    isbnChk = isbnChecker(dataGrid.Rows[ind].Cells[0].Value.ToString());
            }
            if (isbnChk == true)
            {
                int index = e.ColumnIndex;
                string[] sortString = new string[archive.Count()];
                int i = 0;
                switch (index)
                {
                    case 0:
                        foreach (Book book in archive)
                        {
                            sortSystem.Add(book.isbn, book);
                            sortString[i] = book.isbn;
                            i++;
                        }
                        break;
                    case 1:
                        foreach (Book book in archive)
                        {
                            sortSystem.Add(book.author, book);
                            sortString[i] = book.author;
                            i++;
                        }
                        break;
                    case 2:
                        foreach (Book book in archive)
                        {
                            sortSystem.Add(book.name, book);
                            sortString[i] = book.name;
                            i++;
                        }
                        break;
                    case 3:
                        foreach (Book book in archive)
                        {
                            sortSystem.Add(book.publisher, book);
                            sortString[i] = book.publisher;
                            i++;
                        }
                        break;
                    case 4:
                        foreach (Book book in archive)
                        {
                            sortSystem.Add(book.year, book);
                            sortString[i] = book.year;
                            i++;
                        }
                        break;
                    case 5:
                        foreach (Book book in archive)
                        {
                            sortSystem.Add(book.price, book);
                            sortString[i] = book.price;
                            i++;
                        }
                        break;
                }
                IEnumerable<string> sortedStr = from word in sortString orderby word.Length, word.Substring(0, 1) select word;
                int c = 0;
                foreach (string str in sortedStr)
                {
                    sortString[c] = str;
                    c++;
                }
                for (int j = 0; j < i; j++)
                {
                    dataGrid.Rows[j].Cells[0].Value = sortSystem[sortString[j]].isbn;
                    dataGrid.Rows[j].Cells[1].Value = sortSystem[sortString[j]].author;
                    dataGrid.Rows[j].Cells[2].Value = sortSystem[sortString[j]].name;
                    dataGrid.Rows[j].Cells[3].Value = sortSystem[sortString[j]].publisher;
                    dataGrid.Rows[j].Cells[4].Value = sortSystem[sortString[j]].year;
                    dataGrid.Rows[j].Cells[5].Value = sortSystem[sortString[j]].price;
                }
                sortSystem.Clear();
            }
            else
            {
                MessageBox.Show("Incorrect ISBN");
            }
        }

        private void dataGrid_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            var counter = dataGrid.Rows.Count;
            archive.Clear();
            for (int i = 0; i < counter - 1; i++)
            {
                Book book = new Book("", "", "", "", "", "");
                Switcher(dataGrid.Rows[i].Cells[0], ref book.isbn);
                Switcher(dataGrid.Rows[i].Cells[1], ref book.author);
                Switcher(dataGrid.Rows[i].Cells[2], ref book.name);
                Switcher(dataGrid.Rows[i].Cells[3], ref book.publisher);
                Switcher(dataGrid.Rows[i].Cells[4], ref book.year);
                Switcher(dataGrid.Rows[i].Cells[5], ref book.price);
                archive.Add(book);
            }
        }
        public bool isbnChecker(string input)
        {
            int j = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != '-')
                {
                    j++;
                }
                
            }
            if ((j == 10) || (j == 13))
                return true;
            else
                return false;
        }
    }
}
