﻿using System;
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
    public partial class FormMain : Form
    {
        const string wordsFile = "Words.txt";
        const string logFile = "Log.txt";
        List<string> words = new List<string>();
        List<WordCounter> newWords = new List<WordCounter>();
        List<WordCounter> existsWords = new List<WordCounter>();
        int newWordsCount;

        public FormMain()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lexicon\nВерсия 1.0 (06.01.2019)\nАвтор: Сергей Гордеев\n\n" +
                "Создание словаря из текста. Просто вводите любой текст и из него формируется словарь.",
                "О Lexicon");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            enterToolStripMenuItem_Click(null, null);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Загрузка словаря
            try
            {
                words = System.IO.File.ReadAllLines(wordsFile).ToList();
                RefreshList();
            }
            catch { }
        }

        private void enterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            newWords.Clear();
            existsWords.Clear();
            //Поиск слов
            string word = "";
            foreach (char later in textBox.Text + ".") //Точка нужна для того чтоб последнее слово не терялось
            {
                if ((later >= 'a' & later <= 'z') |
                    (later >= 'A' & later <= 'Z') |
                    (later >= 'а' & later <= 'я') |
                    (later >= 'А' & later <= 'Я') |
                    later == 'ё' | later == 'Ё')
                    word += later;
                else
                    if (word.Length > 0)
                {
                    word = word.ToLower();
                    if (words.Find(w => w == word) == null)
                    {
                        WordCounter newWord = newWords.Find(w => w.word == word);
                        if (newWord != null) newWord.Count++;
                        else newWords.Add(new WordCounter(word));
                    }
                    else
                    {
                        WordCounter newWord = existsWords.Find(w => w.word == word);
                        if (newWord != null) newWord.Count++;
                        else existsWords.Add(new WordCounter(word));
                    }
                    word = "";
                    count++;
                }
            }
            foreach (WordCounter newWord in newWords)
            {
                words.Add(newWord.word);
                newWordsCount++;
            }
            textBox.Text = "";
            //Обновление
            words.Sort();
            System.IO.File.WriteAllLines(wordsFile, words);
            RefreshList();
            //Тут можно показать какую-нибудь статистику
            FormStatistic form = new FormStatistic(newWords, existsWords, count);
            form.ShowDialog();
        }

        void RefreshList()
        {
            listView.BeginUpdate();
            listView.Items.Clear();
            foreach (string word in words)
                listView.Items.Add(new ListViewItem(word));
            listView.EndUpdate();
            toolStripStatusLabel.Text = "Всего слов в словаре: " + words.Count +
                "     Сегодня добавлено: " + newWordsCount;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Добавим информацию в лог
            try
            {
                System.IO.File.AppendAllText(logFile, DateTime.Now.ToString("yyyy.MM.dd-hh:mm - ") + 
                    "Всего слов в словаре: " + words.Count + "; Добавлено: " + newWordsCount + "\n");
            }
            catch
            {
                MessageBox.Show("Не получилось сделать запись в лог-файл", "Ошибка");
            }
        }
    }
}
