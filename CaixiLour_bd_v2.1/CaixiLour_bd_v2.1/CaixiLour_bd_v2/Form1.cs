using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

using System.Text;
using System.Linq;
using System.IO;

namespace CaixiLour_bd_v2
{
    public partial class CAIXIOUR : Form
    {
        //adapters exclusivos
        int maxrows;
        SqlDataAdapter da_tab;
        PictureBox[] pb_array = new PictureBox[2];
        //int maxrows;
        SqlConnection cnn;  //coneção
        string sql_string;  //select ... from

        //DataSet dat_set_tab;
        DataTable dat_tab_tab;

        public CAIXIOUR()
        {
             InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pes_preco();
            pre_pes_tb();
        }
        // voids
        private void pre_pes_tb()
        {
            //parte grafica
            panel_pes_complementos.Visible = false;
            panel_pes_portas.Visible = false;
            panel_pes_puxadores.Visible = false;
            panel_pes_estores.Visible = false;

            //verifica a tab para abrir
            if (rb_portas.Checked == true)
            {
                panel_pes_portas.Visible = true;
                sql_string = "select * from portas";
            }
            if (rb_estores.Checked == true)
            {
                panel_pes_estores.Visible = true;
                sql_string = "select * from estores";
            }
            if (rb_puxadores.Checked == true)
            {
                panel_pes_puxadores.Visible = true;
                sql_string = "select * from puxadores";
            }
            if (rb_complemetos.Checked == true)
            {
                panel_pes_complementos.Visible = true;
                sql_string = "select * from complementos";
            }
            pes_tb();
        }
        public void pes_tb()
        {
            cnn = new SqlConnection("Data Source=192.168.3.13,1433; Network Library=DBMSSOCN;Initial Catalog=caixilour_estoque; User ID=admin;Password=caixilour");
            ////ligar tab
            cnn.Open();
            da_tab = new SqlDataAdapter(sql_string, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            maxrows = dat_tab_tab.Rows.Count;
            cnn.Close();

            PictureBox[] pb_array = new PictureBox[maxrows];//array de ing
            int n;
            int i=0;
            int x = 0;
           
            panel_menu.Controls.Clear();
         
            for (n= 0; n <= maxrows-1; n++ )
            {
                // comverte byte ei img
                 Byte[] fotos = (byte[])dat_tab_tab.Rows[n]["Imagem"];
                MemoryStream ms =new MemoryStream(fotos);
                Image fotos_s = Image.FromStream(ms);

                //cria pb no panel_menu
                x++;
                pb_array[n] = new PictureBox(); 
                pb_array[n].Location = new Point(16+((x-1)*155),10+(146*i));
                pb_array[n].Size = new Size(133, 140);
                pb_array[n].SizeMode = PictureBoxSizeMode.Zoom;
                pb_array[n].Image =fotos_s;
                pb_array[n].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                pb_array[n].Name =Convert.ToString(n);
                this.Controls.Add(panel_menu);
                pb_array[n].Click += new EventHandler(this.click_fotos_Click);
                panel_menu.Controls.Add(pb_array[n]);
                if (x == 5)
                {
                    x = 0;
                    i = i + 1;
                }
            }
            pes_1_foto_inf();
        }
        private void pes_preco()
        {
            if (rb_portas.Checked == true)
            {
                sql_string = "select  preço  from portas ORDER BY preço DESC";
            }
            if (rb_estores.Checked == true)
            {
                sql_string = "select  preço from estores ORDER BY preço DESC";
            }
            if (rb_puxadores.Checked == true)
            {
                sql_string = "select  preço from puxadores ORDER BY preço DESC";
            }
            if (rb_complemetos.Checked == true)
            {
                sql_string = "select  preço from complementos ORDER BY preço DESC";
            }

            cnn = new SqlConnection("Data Source=192.168.3.13,1433; Network Library=DBMSSOCN;Initial Catalog=caixilour_estoque; User ID=admin;Password=caixilour");
            ////ligar tab
            cnn.Open();
            da_tab = new SqlDataAdapter(sql_string, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            cnn.Close();

            SB_preco.Maximum = (int)Convert.ToDecimal(dat_tab_tab.Rows[0]["Preço"]);
        }
        private void pes_cor()
        {
            if (rb_portas.Checked == true)
            {
                sql_string = "select DISTINCT Cor from portas";
            }
            if (rb_estores.Checked == true)
            {
                sql_string = "select DISTINCT Cor from estores";
            }
            if (rb_puxadores.Checked == true)
            {
                sql_string = "select DISTINCT Cor from puxadores";
            }
            if (rb_complemetos.Checked == true)
            {
                sql_string = "select DISTINCT Cor from complementos";
            }

            cnn = new SqlConnection("Data Source=192.168.3.13,1433; Network Library=DBMSSOCN;Initial Catalog=caixilour_estoque; User ID=admin;Password=caixilour");
            ////ligar tab
            cnn.Open();
            da_tab = new SqlDataAdapter(sql_string, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            //maxrows = dat_tab_tab.Rows.Count;
            cnn.Close();
            if (rb_portas.Checked == true)
            {
                cb_cor_portas.DisplayMember = "Cor";
                cb_cor_portas.ValueMember = "Cor";
                cb_cor_portas.DataSource = dat_tab_tab;
            }
            if (rb_estores.Checked == true)
            {
                cb_cor_estores.DisplayMember = "Cor";
                cb_cor_estores.ValueMember = "Cor";
                cb_cor_estores.DataSource = dat_tab_tab;
            }   
            if (rb_puxadores.Checked == true)
            {
                cb_cor_puxadores.DisplayMember = "Cor";
                cb_cor_puxadores.ValueMember = "Cor";
                cb_cor_puxadores.DataSource = dat_tab_tab;
            }
            if (rb_complemetos.Checked == true)
            {
                cb_cor_complementos.DisplayMember = "Cor";
                cb_cor_complementos.ValueMember = "Cor";
                cb_cor_complementos.DataSource = dat_tab_tab;
            }
        }
        public void click_fotos_Click(object sender, EventArgs e)
        {
            var Pictur = sender as PictureBox;
           for (int i = 0; i < maxrows; i++)
           {
              if (Pictur != null && Pictur.Name == Convert.ToString(i))
             {
                  Byte[] fotos = (byte[])dat_tab_tab.Rows[i]["Imagem"];
                MemoryStream ms =new MemoryStream(fotos);
                Image fotos_s = Image.FromStream(ms);

                 pb_foto.BackgroundImage = fotos_s;
                 l_id.Text = Convert.ToString(dat_tab_tab.Rows[i]["ID"]);
                 l_ref.Text = Convert.ToString(dat_tab_tab.Rows[i]["Referência"]);
                 l_descricao.Text = Convert.ToString(dat_tab_tab.Rows[i]["descriÇão"]);
                 l_cor.Text = Convert.ToString(dat_tab_tab.Rows[i]["cor"]);
                 l_preco.Text = Convert.ToString(dat_tab_tab.Rows[i]["Preço"])+"€";
                 if (rb_portas.Checked==true)
                 {
                   l_tipo_grellha.Text = Convert.ToString(dat_tab_tab.Rows[i]["Tipo Grelha"]);
                   if (Convert.ToString(dat_tab_tab.Rows[i]["Vidro"])=="True")   
                   {
                       panel_d_portas.Visible = true;
                       rb_vidro_s.Checked = true;
                   }
                   else
                   {
                       rb_vidro_nao.Checked = true;
                   }
                   if (Convert.ToString(dat_tab_tab.Rows[i]["Grelha"]) == "True")
                   {
                        rb_grelha_sim.Checked = true;
                       
                   }
                   else
                   {
                       rb_grelha_nao.Checked = true;
                   }
                 }
                 else
                 {
                     panel_d_portas.Visible = false;
                 }
             }       
           }           
        }
        public void pes_1_foto_inf()
        {
            int i = 0;
            if ( dat_tab_tab.Rows.Count != 0)
            {
                Byte[] fotos = (byte[])dat_tab_tab.Rows[i]["Imagem"];
                MemoryStream ms = new MemoryStream(fotos);
                Image fotos_s = Image.FromStream(ms);

                pb_foto.BackgroundImage = fotos_s;
                l_id.Text = Convert.ToString(dat_tab_tab.Rows[i]["ID"]);
                l_ref.Text = Convert.ToString(dat_tab_tab.Rows[i]["Referência"]);
                l_descricao.Text = Convert.ToString(dat_tab_tab.Rows[i]["descriÇão"]);
                l_cor.Text = Convert.ToString(dat_tab_tab.Rows[i]["cor"]);
                l_preco.Text = Convert.ToString(dat_tab_tab.Rows[i]["Preço"]) + "€";
                if (rb_portas.Checked == true)
                {
                    l_tipo_grellha.Text = Convert.ToString(dat_tab_tab.Rows[i]["Tipo Grelha"]);
                    if (Convert.ToString(dat_tab_tab.Rows[i]["Vidro"]) == "True")
                    {
                        panel_d_portas.Visible = true;
                        rb_vidro_s.Checked = true;
                    }
                    else
                    {
                        rb_vidro_nao.Checked = true;
                    }
                    if (Convert.ToString(dat_tab_tab.Rows[i]["Grelha"]) == "True")
                    {
                        rb_grelha_sim.Checked = true;
                    }
                    else
                    {
                        rb_grelha_nao.Checked = true;
                    }
                }
                else
                {
                    panel_d_portas.Visible = false;
                }
            }
            else
            {
                pb_foto.BackgroundImage =CaixiLour_bd_v2.Properties.Resources.vazio ;
                l_id.Text ="";
                l_ref.Text ="";
                l_descricao.Text = "";
                l_cor.Text = "";
                l_preco.Text ="€";
                 if (rb_portas.Checked == true)
                  {
                    l_tipo_grellha.Text = "";
                    panel_d_portas.Visible = true;
                  }        
                else
                 {
                     panel_d_portas.Visible = false;
                 }      
            }
        }
        //voids de b_pes_Click
        public void pes_portas()
        {
            string sql_s = "";
            sql_string = "select * from portas";
            if (tb_preco.Text != "")
            {
                if ((int)Convert.ToDecimal(tb_preco.Text) <= SB_preco.Maximum)//verifica se o preso e valido
                {
                    SB_preco.Value = (int)Convert.ToDecimal(tb_preco.Text);
                }
                else
                {
                    MessageBox.Show("So ha produtos que custem " + Convert.ToString(SB_preco.Maximum + "€"), "Preço", MessageBoxButtons.OK);
                    SB_preco.Value = (SB_preco.Maximum);
                }
                lb_preco_total.Text = "0€ - " + Convert.ToString(SB_preco.Value) + "€";
            }

            //pes por cor
            if (cb_cor_portas.Items.Count != 0)
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Cor ='" + (cb_cor_portas.Text) + "'";
            }
            //pes por preco
            if (Convert.ToString(SB_preco.Value) != "0")
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "preço <='" + (Convert.ToString(SB_preco.Value));
            }

            //rb_portas_vidro
            if (rb_portas_vidro_sim.Checked == true)
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Vidro ='true'";
            }
            if (rb_portas_vidro_nao.Checked == true)
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Vidro ='false'";
            }

            //rb_portas_grlha
            if (rb_portas_grlha_sim.Checked == true)
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Grelha ='true'";
            }
            if (rb_portas_grlha_nao.Checked == true)
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Grelha ='false'";
            }

            //pes por ref
            if (tb_pes_portas.Text != "")
            {

                sql_s = "Referência ='" + (tb_pes_portas.Text) + "'";
            }

            //mota a  sql_string
            if (sql_s != "")
            {
                sql_string = "select * from portas where " + sql_s;
            }
            pes_tb();//pes se tiver serto
        }

        //rb
        private void rb_portas_CheckedChanged(object sender, EventArgs e)
        {
            panel_d_portas.Visible = true;
            pre_pes_tb();//abre e mostra tab
        }
        private void rb_puxadores_CheckedChanged_1(object sender, EventArgs e)
        {
            panel_d_portas.Visible = false;
            pre_pes_tb();
        }
        private void rb_estores_CheckedChanged(object sender, EventArgs e)
        {
            panel_d_portas.Visible = false;
            pre_pes_tb();
        }
        private void rb_complemetos_CheckedChanged(object sender, EventArgs e)
        {
            panel_d_portas.Visible = false;
            pre_pes_tb();
        }    
        private int strlen(object p)
        {
            throw new NotImplementedException();
        }//mostra o resiltado das tabs
        private MemoryStream MemoryStream(byte[] fotos)
        {
            throw new NotImplementedException();
        }
        //b_pes
        private void b_pes_complemetos_Click(object sender, EventArgs e)
        {
            if (tb_preco.Text != "")
            {
                if ((int)Convert.ToDecimal(tb_preco.Text) <= SB_preco.Maximum)//verifica se o preso e valido
                {
                    SB_preco.Value = (int)Convert.ToDecimal(tb_preco.Text);
                }
                else
                {
                    MessageBox.Show("So ha produtos que custem " + Convert.ToString(SB_preco.Maximum + "€"), "Preço", MessageBoxButtons.OK);
                    SB_preco.Value = (SB_preco.Maximum);
                }
                lb_preco_total.Text = "0€ - " + Convert.ToString(SB_preco.Value) + "€";
            }
            if (tb_pes_complemetos.Text == "" && cb_cor_complementos.Items.Count != 0 && Convert.ToString(SB_preco.Value) == "0")//pes por cor
            {
              sql_string = "select * from complementos where Cor ='" + (cb_cor_complementos.Text) + "'";
            }
            if (tb_pes_complemetos.Text != "" && cb_cor_complementos.Items.Count == 0 && Convert.ToString(SB_preco.Value) == "0")//pes por ref
            {
                sql_string = "select * from complementos where Referência ='" + (tb_pes_complemetos.Text) + "'";
            }
            if (tb_pes_complemetos.Text == "" && cb_cor_complementos.Items.Count == 0 && Convert.ToString(SB_preco.Value) != "0")//pes por preço
            {
                sql_string = "select * from complementos where preço <='" + (Convert.ToString(SB_preco.Value)) + "'";
            }
            if (tb_pes_complemetos.Text == "" && cb_cor_complementos.Items.Count != 0 && Convert.ToString(SB_preco.Value) != "0")//pes por preço e cor
            {
                sql_string = "select * from complementos where preço <='" + (Convert.ToString(SB_preco.Value)) + "' and " + "Cor ='" + (cb_cor_complementos.Text) + "'";
            }

             pes_tb();//pes se tiver serto
        }
        private void b_pes_estores_Click(object sender, EventArgs e)
        {
            if (tb_preco.Text != "")
            {
                if ((int)Convert.ToDecimal(tb_preco.Text) <= SB_preco.Maximum)//verifica se o preso e valido
                {
                    SB_preco.Value = (int)Convert.ToDecimal(tb_preco.Text);
                }
                else
                {
                    MessageBox.Show("So ha produtos que custem " + Convert.ToString(SB_preco.Maximum + "€"), "Preço", MessageBoxButtons.OK);
                    SB_preco.Value = (SB_preco.Maximum);
                }
                lb_preco_total.Text = "0€ - " + Convert.ToString(SB_preco.Value) + "€";
            }
            if (tb_pes_estores.Text == "" && cb_cor_estores.Items.Count != 0 && Convert.ToString(SB_preco.Value) == "0")//pes por cor
            {
                sql_string = "select * from estores where Cor ='" + (cb_cor_estores.Text) + "'";
            }
            if (tb_pes_estores.Text != "" && cb_cor_estores.Items.Count == 0 && Convert.ToString(SB_preco.Value) == "0")//pes por ref
            {
                sql_string = "select * from estores where Referência ='" + (tb_pes_complemetos.Text) + "'";
            }
            if (tb_pes_estores.Text == "" && cb_cor_estores.Items.Count == 0 && Convert.ToString(SB_preco.Value) != "0")//pes por preço
            {
                sql_string = "select * from estores where preço <='" + (Convert.ToString(SB_preco.Value)) + "'";
            }
            if (tb_pes_estores.Text == "" && cb_cor_estores.Items.Count != 0 && Convert.ToString(SB_preco.Value) != "0")//pes por preço e cor
            {
                sql_string = "select * from estores where preço <='" + (Convert.ToString(SB_preco.Value)) + "' and " + "Cor ='" + (cb_cor_estores.Text) + "'";
            }

                pes_tb();//pes se tiver serto
        }
        private void b_pes_puxadores_Click(object sender, EventArgs e)
        {
            if (tb_preco.Text != "")
            {
                if ((int)Convert.ToDecimal(tb_preco.Text) <= SB_preco.Maximum)//verifica se o preso e valido
                {
                    SB_preco.Value = (int)Convert.ToDecimal(tb_preco.Text);
                }
                else
                {
                    MessageBox.Show("So ha produtos que custem " + Convert.ToString(SB_preco.Maximum + "€"), "Preço", MessageBoxButtons.OK);
                    SB_preco.Value = (SB_preco.Maximum);
                }
                lb_preco_total.Text = "0€ - " + Convert.ToString(SB_preco.Value) + "€";
            }
            if (tb_pes_puxadores.Text == "" && cb_cor_puxadores.Items.Count != 0 && Convert.ToString(SB_preco.Value) == "0")//pes por cor
            {
                sql_string = "select * from puxadores where Cor ='" + (cb_cor_puxadores.Text) + "'";
            }
            if (tb_pes_puxadores.Text != "" && cb_cor_puxadores.Items.Count == 0 && Convert.ToString(SB_preco.Value) == "0")//pes por ref
            {
                sql_string = "select * from puxadores where Referência ='" + (tb_pes_complemetos.Text) + "'";
            }
            if (tb_pes_puxadores.Text == "" && cb_cor_puxadores.Items.Count == 0 && Convert.ToString(SB_preco.Value) != "0")//pes por preço
            {
                sql_string = "select * from puxadores where preço <='" + (Convert.ToString(SB_preco.Value)) + "'";
            }
            if (tb_pes_puxadores.Text == "" && cb_cor_puxadores.Items.Count != 0 && Convert.ToString(SB_preco.Value) != "0")//pes por preço e cor
            {
                sql_string = "select * from puxadores where preço <='" + (Convert.ToString(SB_preco.Value)) + "' and " + "Cor ='" + (cb_cor_puxadores.Text) + "'";
            }

                pes_tb();//pes se tiver serto
        }
        private void b_pes_portas_Click(object sender, EventArgs e)
        {
            pes_portas(); 
        }
        //menu
        private void panel_menu_Paint(object sender, PaintEventArgs e)
        {
            panel_menu.AutoScroll = true;
            Rectangle r = this.ClientRectangle;

        }//SECROL

        public DataSet at_set_tab { get; set; }
        public int n { get; set; }
        public byte[] fotos { get; set; }
        public ImageLayout Stretch { get; set; }
        private void panel_pes_tabs_Paint(object sender, PaintEventArgs e)
        {  }
        //cb_cor
        private void cb_cor_puxadores_Click(object sender, EventArgs e)
        {
            if (cb_cor_puxadores.Items.Count==0)
            {
             pes_cor();   
            }  
        }
        private void cb_cor_complementos_Click(object sender, EventArgs e)
        {
            if (cb_cor_complementos.Items.Count == 0)
            {
                pes_cor();
            }
        }
        private void cb_cor_portas_Click(object sender, EventArgs e)
        {
            if (cb_cor_portas.Items.Count == 0)
            {
                pes_cor();  
            }
        }
        private void cb_cor_estores_Click(object sender, EventArgs e)
        {
            if (cb_cor_estores.Items.Count == 0)
            {
                pes_cor();
            }
        }
        //sb_preco
        private void SB_preco_Scroll(object sender, ScrollEventArgs e)
        {
            lb_preco_total.Text = "0€ - " + Convert.ToString(SB_preco.Value) + "€";
            if (rb_portas.Checked == true)
            {
                panel_pes_portas.Visible = true;
                sql_string = "select * from portas where preço <='" + (Convert.ToString(SB_preco.Value)) + "'";
            }
            if (rb_estores.Checked == true)
            {
                panel_pes_estores.Visible = true;
                sql_string = "select * from estores where preço <='" + (Convert.ToString(SB_preco.Value)) + "'";
            }
            if (rb_puxadores.Checked == true)
            {
                panel_pes_puxadores.Visible = true;
                sql_string = "select * from puxadores where preço <='" + (Convert.ToString(SB_preco.Value)) + "'";
            }
            if (rb_complemetos.Checked == true)
            {
                panel_pes_complementos.Visible = true;
                sql_string = "select * from complementos where preço <='" + (Convert.ToString(SB_preco.Value)) + "'";
            }
            pes_tb();
        }
        private void ll_website_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.janelaseportas.com");

        }//website
        private void ll_facebook_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/Caixilour/");
        }//facebook

        public BorderStyle FixedSingle { get; set; }
        //rb portas 
        private void rb_portas_vidro_nao_CheckedChanged(object sender, EventArgs e)
        {
            pes_portas();
        }
        private void rb_portas_grlha_sim_CheckedChanged(object sender, EventArgs e)
        {
            pes_portas();
        }
        private void rb_portas_vidro_sim_CheckedChanged(object sender, EventArgs e)
        {
            pes_portas();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pes_portas();
        }
    }
}