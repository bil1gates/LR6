using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace органайзер
{
    public partial class Form2 : Form
    {
        private readonly string excelSavePath = @"E:\ТРПО_КП";
        public Form2()
        {
            InitializeComponent();
        }

        private void mainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (mainDataGridView.Rows.Count > 1)
                {
                    mainDataGridView.Rows.RemoveAt(mainDataGridView.CurrentRow.Index);
                }
            }
            catch (Exception s)
            {

                MessageBox.Show(s.Message);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int up_stazh = Convert.ToInt32(textBox3.Text);
            decimal percent = 1 + (Convert.ToDecimal(percentOkladTextBox.Text) / 100);
            for (int i = 0; i < mainDataGridView.Rows.Count; i++)
            {
                if (Convert.ToInt32(mainDataGridView[7, i].Value) > up_stazh) mainDataGridView[6, i].Value = Convert.ToDecimal(mainDataGridView[6, i].Value) * percent;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (mainDataGridView.Rows.Count > 1)
                {
                    if (mainDataGridView.CurrentCell.ColumnIndex == 1 && percentOkladTextBox.Text != "")
                    {
                        decimal salary_to_decrease = Convert.ToDecimal(mainDataGridView.CurrentRow.Cells[3].Value);
                        salary_to_decrease *= 1 - (Convert.ToDecimal(percentOkladTextBox.Text) / 100);
                        salary_to_decrease = Math.Round(salary_to_decrease, 2);


                        mainDataGridView.CurrentRow.Cells[3].Value = salary_to_decrease;

                    }
                    else
                    {
                        MessageBox.Show("проверьте введенные данные");
                    }
                }
            }
            catch (Exception s)
            {

                MessageBox.Show(s.Message);
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (mainDataGridView.Rows.Count > 1)
                {
                    int kolvo_oklad = 0;
                    int oklad_value_kolvo = Convert.ToInt32(textBox2.Text);
                    for (int i = 0; i < mainDataGridView.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(mainDataGridView[6, i].Value) < oklad_value_kolvo && String.IsNullOrEmpty(Convert.ToString(mainDataGridView[6, i].Value))==false) kolvo_oklad++;
                    }
                    label2.Text = "Количество:" + kolvo_oklad;
                }
            }
            catch (Exception s)
            {

                MessageBox.Show(s.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application appExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workBookExcel = appExcel.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel.Worksheet worksheetExcel = null;
                appExcel.Visible = true;
                worksheetExcel = workBookExcel.Sheets[1];
                worksheetExcel = workBookExcel.ActiveSheet;
                worksheetExcel.Name = "Таблица 1";
                //Копируем заголовки
                for (int i = 1; i < mainDataGridView.Columns.Count + 1; i++)
                {
                    worksheetExcel.Cells[1, i] = mainDataGridView.Columns[i - 1].HeaderText;
                }
                //Заполняем таблицу
                for (int i = 0; i < mainDataGridView.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < mainDataGridView.Columns.Count; j++)
                    {
                        worksheetExcel.Cells[i + 2, j + 1] = mainDataGridView.Rows[i].Cells[j].Value.ToString();
                    }
                }
                //Сохраняем
                workBookExcel.SaveAs(excelSavePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                appExcel.Quit();
            }
            catch (Exception s)
            {

                MessageBox.Show(s.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                float sum = 0;
                for (int i = 0; i < mainDataGridView.Rows.Count; i++)
                    sum += (float)mainDataGridView[3, i].Value;
                float result = sum / mainDataGridView.Rows.Count;
                //label3.Text = "Сумма:" + Convert.ToString(result);
            }
            catch (Exception s)
            {

                MessageBox.Show(s.Message);
            }

        }
      
        private void button2_Click(object sender, EventArgs e)
        {
                    int sum_stazh = 0;
                       for (int i = 0; i < mainDataGridView.Rows.Count; i++)
                       {
                           sum_stazh += Convert.ToInt32(mainDataGridView[7, i].Value); 
                       }
                    decimal sred = sum_stazh / (mainDataGridView.Rows.Count-1);
                    label4.Text = Convert.ToString(sred);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            TimeSpan age;
            for (int i = 1; i < mainDataGridView.Rows.Count; i++)
            {
                if ((string)mainDataGridView[1, i].Value == "Мужской" || (string)mainDataGridView[1, i].Value == "мужской" || (string)mainDataGridView[1, i].Value == "м" || (string)mainDataGridView[1, i].Value == "М")
                {
                    age = now - Convert.ToDateTime(mainDataGridView[3, i].Value);
                    if ((int)(age.Days / 365.2425) > 65) mainDataGridView.Rows.RemoveAt(i);
                }
               else if ((string)mainDataGridView[1, i].Value == "Женский" || (string)mainDataGridView[1, i].Value == "женский" || (string)mainDataGridView[1, i].Value == "ж" || (string)mainDataGridView[1, i].Value == "Ж")
                {
                    age = now - Convert.ToDateTime(mainDataGridView[3, i].Value);
                    if ((int)(age.Days / 365.2425) > 60) mainDataGridView.Rows.RemoveAt(i);
               }
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void percentOkladTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            TimeSpan stazh;
            for (int i = 0; i < mainDataGridView.Rows.Count; i++)
            {
                if (String.IsNullOrEmpty(Convert.ToString(mainDataGridView[4, i].Value)) == false)
                {
                    stazh = now - Convert.ToDateTime(mainDataGridView[4, i].Value);
                    mainDataGridView[7, i].Value = (int) (stazh.Days / 365.2425);
                }
            }
        }
    }
}
