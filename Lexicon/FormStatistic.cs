using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lexicon
{
    public partial class FormStatistic : Form
    {
        public FormStatistic(List<WordCounter> newWords, List<WordCounter> existsWords, int count )
        {
            //newWords.Sort();
            //existsWords.Sort();
            InitializeComponent();
            label4.Text = newWords.Count.ToString();
            label5.Text = (newWords.Count + existsWords.Count).ToString();
            label6.Text = count.ToString();
            foreach (WordCounter word in newWords)
            {
                ListViewItem item = new ListViewItem(word.word);
                item.SubItems.Add(word.Count.ToString());
                listViewNew.Items.Add(item);
            }
            foreach (WordCounter word in existsWords)
            {
                ListViewItem item = new ListViewItem(word.word);
                item.SubItems.Add(word.Count.ToString());
                listViewExists.Items.Add(item);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
