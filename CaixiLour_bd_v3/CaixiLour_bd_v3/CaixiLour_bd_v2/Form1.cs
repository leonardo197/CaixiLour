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
       Boolean b=false;
       string st = "portas";
        
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
            pre_pes_tb();
            pes_cor();
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
            if (rb_assitencias.Checked == true)
            {
                panel_pes_assitencias.Visible = true;
                sql_string = "select * from assistencias";
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
                int i = 0;
                int x = 0;

                panel_menu.Controls.Clear();

                for (n = 0; n <= maxrows - 1; n++)
                {
                    // comverte byte ei img
                    Byte[] fotos = (byte[])dat_tab_tab.Rows[n]["Imagem"];
                    MemoryStream ms = new MemoryStream(fotos);
                    Image fotos_s = Image.FromStream(ms);
                    //cria pb no panel_menu
                    x++;
                    pb_array[n] = new PictureBox();
                    pb_array[n].Location = new Point(16 + ((x - 1) * 155), 10 + (146 * i));
                    pb_array[n].Size = new Size(133, 140);
                    pb_array[n].SizeMode = PictureBoxSizeMode.Zoom;
                    pb_array[n].Image = fotos_s;
                    //pb_array[n].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    pb_array[n].Name = Convert.ToString(n);
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
        public void pes_familia()
        {
            b = false;
            if (rb_estores.Checked == true)
            {
                sql_string = "select DISTINCT Família from estores";
            }
            if (rb_puxadores.Checked == true)
            {
                sql_string = "select DISTINCT Família from puxadores";
            }
            if (rb_complemetos.Checked == true)
            {
                sql_string = "select DISTINCT Família from complementos";
            }
            if (rb_assitencias.Checked == true)
            {
                sql_string = "select DISTINCT Família from assistencias";
            }
            //ligar tab
            cnn = new SqlConnection("Data Source=192.168.3.13,1433; Network Library=DBMSSOCN;Initial Catalog=caixilour_estoque; User ID=admin;Password=caixilour");
            cnn.Open();
            da_tab = new SqlDataAdapter(sql_string, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            //maxrows = dat_tab_tab.Rows.Count;
            cnn.Close();
            if (rb_estores.Checked == true)
            {
                cb_familia_estores.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_familia_estores.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Família"]));
                }
                cb_familia_estores.Items.Add("TODAS");
                cb_familia_estores.Text = Convert.ToString("TODAS");
            }
            if (rb_puxadores.Checked == true)
            {
                cb_familia_puxadores.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_familia_puxadores.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Família"]));
                }
                cb_familia_puxadores.Items.Add("TODAS");
                cb_familia_puxadores.Text = Convert.ToString("TODAS");
            }
            if (rb_complemetos.Checked == true)
            {
                cb_familia_complementos.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_familia_complementos.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Família"]));
                }
                cb_familia_complementos.Items.Add("TODAS");
                cb_familia_complementos.Text = Convert.ToString("TODAS");
            }
            if (rb_assitencias.Checked == true)//assitencias
            {
                cb_familia_assitencias.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_familia_assitencias.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Família"]));
                }
                cb_familia_assitencias.Items.Add("TODAS");
                cb_familia_assitencias.Text = Convert.ToString("TODAS");
            }
            b = true;
        }
        private void pes_cor()
        {
            b = false;
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
            ////ligar tab
            cnn = new SqlConnection("Data Source=192.168.3.13,1433; Network Library=DBMSSOCN;Initial Catalog=caixilour_estoque; User ID=admin;Password=caixilour");
            cnn.Open();
            da_tab = new SqlDataAdapter(sql_string, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            //maxrows = dat_tab_tab.Rows.Count;
            cnn.Close();
            if (rb_portas.Checked == true)
            {
                cb_cor_portas.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_cor_portas.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Cor"]));
                }
                cb_cor_portas.Items.Add("TODAS");
                cb_cor_portas.Text = Convert.ToString("TODAS");
            }
            if (rb_estores.Checked == true)
            {
                cb_cor_estores.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_cor_estores.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Cor"]));
                }
                cb_cor_estores.Items.Add("TODAS");
                cb_cor_estores.Text = Convert.ToString("TODAS");
            }   
            if (rb_puxadores.Checked == true)
            {
                cb_cor_puxadores.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_cor_puxadores.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Cor"]));
                }
                cb_cor_puxadores.Items.Add("TODAS");
                cb_cor_puxadores.Text = Convert.ToString("TODAS");
            }
            if (rb_complemetos.Checked == true)
            {
                cb_cor_complementos.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_cor_complementos.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Cor"]));
                }
                cb_cor_complementos.Items.Add("TODAS");
                cb_cor_complementos.Text = Convert.ToString("TODAS");
            }
            b = true;
        }
        public void click_fotos_Click(object sender, EventArgs e)
        {
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
            cnn = new SqlConnection("Data Source=192.168.3.13,1433; Network Library=DBMSSOCN;Initial Catalog=caixilour_estoque; User ID=admin;Password=caixilour");
            ////ligar tab
            cnn.Open();
            da_tab = new SqlDataAdapter(sql_string, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            maxrows = dat_tab_tab.Rows.Count;
            cnn.Close();
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
                 l_descricao.Text = Convert.ToString(dat_tab_tab.Rows[i]["descrição"]);
                 if (rb_portas.Checked != true)
                 {
                     l_familia.Text = Convert.ToString(dat_tab_tab.Rows[i]["Família"]);
                 }
                 if (rb_assitencias.Checked!=true)
                 {
                  l_cor.Text = Convert.ToString(dat_tab_tab.Rows[i]["cor"]);
                 }
               
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
                if (rb_portas.Checked!=true)
                {
                    l_familia.Text = Convert.ToString(dat_tab_tab.Rows[i]["Família"]);   
                }
                if (rb_portas.Checked==true )
                {
                  l_preco1.Text = Convert.ToString(dat_tab_tab.Rows[i]["Preço"]) + "€";  
                }
                l_preco.Text = Convert.ToString(dat_tab_tab.Rows[i]["Preço"]) + "€";
                if (rb_assitencias.Checked!=true )
                {
                 l_cor.Text = Convert.ToString(dat_tab_tab.Rows[i]["cor"]);
                }
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

            //pes por cor
            if (cb_cor_portas.Items.Count != 0 && cb_cor_portas.Text!="TODAS" )
            {
                sql_s = sql_s + "Cor ='" + (cb_cor_portas.Text) + "'";
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
        public void pes_estores()
        {
            sql_string = "select * from estores";
            string sql_s = "";
            
            if (cb_cor_estores.Text!="TODAS")//pes por cor
            {
                sql_s = "Cor ='" + (cb_cor_estores.Text) + "'";
            }
            if (cb_familia_estores.Text != "TODAS")//pes por Família
            {
             if (sql_s != "")
              {
                 sql_s = sql_s + " and ";
              }
             sql_s = sql_s + "Família'"+(cb_familia_estores.Text)+"'";
            }
            //mota a  sql_string
            if (sql_s != "")
            {
                sql_string = "select * from estores where " + sql_s;
            }
            pes_tb();//pes se tiver serto
        }
        public void pes_puxadores()
             {
                 sql_string = "select * from puxadores";
                 string sql_s = "";

                 if (cb_cor_puxadores.Text != "TODAS")//pes por cor
                 {
                     sql_s = "Cor ='" + (cb_cor_puxadores.Text) + "'";
                 }
                 if (cb_familia_puxadores.Text != "TODAS")//pes por Família
                 {
                     if (sql_s != "")
                     {
                         sql_s = sql_s + " and ";
                     }
                     sql_s = sql_s + "Família='" + (cb_familia_puxadores.Text) + "'";
                 }
                 //mota a  sql_string
                 if (sql_s != "")
                 {
                     sql_string = "select * from puxadores where " + sql_s;
                 }
                 pes_tb();//pes se tiver serto
        }
        public void pes_complementos()
        {
            sql_string = "select * from complementos";
            string sql_s = "";

            if (cb_cor_complementos.Text != "TODAS")//pes por cor
            {
                sql_s = "Cor ='" + (cb_cor_complementos.Text) + "'";
            }
            if (cb_familia_complementos.Text != "TODAS")//pes por Família
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Família='" + (cb_familia_complementos.Text) + "'";
            }
            //mota a  sql_string
            if (sql_s != "")
            {
                sql_string = "select * from complementos where " + sql_s;
            }
            pes_tb();//pes se tiver serto

            
        }
        public void pes_assitencias()
        {
            sql_string = "select * from assistencias";
            string sql_s = "";

            if (cb_familia_assitencias.Text != "TODAS")//pes por Família
            {
                sql_s = "Família='" + (cb_familia_assitencias.Text) + "'";
            }
            //mota a  sql_string
            if (sql_s != "")
            {
                sql_string = "select * from assitencias where " + sql_s;
            }
            pes_tb();//pes se tiver serto
        }
        //rb
        private void rb_portas_CheckedChanged(object sender, EventArgs e)
        {
            if (st!="Portas")
            {
             panel_pes_assitencias.Visible = false;
             panel_d_portas.Visible = true;
            pre_pes_tb();//abre e mostra tab
            pes_cor();
            st = "Portas";
            }
        }
        private void rb_puxadores_CheckedChanged_1(object sender, EventArgs e)
        {
            if (st!="puxadores")
            {
            panel2.Location = new Point(0, 460);
            panel_pes_assitencias.Visible = false;
            panel_d_portas.Visible = false;
            pre_pes_tb();
            pes_cor();
            pes_familia();
            st = "puxadores"; 
            }  
        }
        private void rb_estores_CheckedChanged(object sender, EventArgs e)
        {
            if (st != "estores")
            {
             panel2.Location = new Point(0, 460);
             panel_pes_assitencias.Visible = false;
             panel_d_portas.Visible = false;
             pre_pes_tb();
             pes_cor();
             pes_familia();
             st = "estores";
            }  
        }
        private void rb_complemetos_CheckedChanged(object sender, EventArgs e)
        {
            if (st != "complemetos")
            {
             panel2.Location = new Point(0, 460);
             panel_pes_assitencias.Visible = false;
             panel_d_portas.Visible = false;
             pre_pes_tb();
             pes_cor();
             pes_familia();
             st = "complemetos";
            }    
        }
        private void rb_despesas_CheckedChanged(object sender, EventArgs e)
        {
            if (st != "despesas")
            {
                panel2.Location = new Point(0, 460);
                panel_pes_assitencias.Visible = false;
                panel_d_portas.Visible = false;
                pre_pes_tb();
                pes_cor();
                st = "despesas";
            } 
        }
        private void rb_assitencias_CheckedChanged_1(object sender, EventArgs e)
        {
           if (st != "assitencias")
            {
                panel2.Location = new Point(0, 417);//453
                panel_pes_assitencias.Visible = true;
                panel_d_portas.Visible = false;
                pre_pes_tb();
                pes_familia();
                st = "assitencias";
            }
        }
        private int strlen(object p)
        {
            throw new NotImplementedException();
        }//mostra o resiltado das tabs
        private MemoryStream MemoryStream(byte[] fotos)
        {
            throw new NotImplementedException();
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
        private void cb_cor_portas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b==true)
            {
              pes_portas();  
            }           
        }
        private void cb_cor_estores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_estores();
            }
        }
        private void cb_cor_complementos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_complementos();
            }
        }
        private void cb_cor_puxadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_puxadores();
            }
        }
 
        public BorderStyle FixedSingle { get; set; }
        //rb portas 
        private void rb_portas_vidro_nao_CheckedChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_portas();
            }
        }
        private void rb_portas_grlha_sim_CheckedChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_portas();
            }
        }
        private void rb_portas_vidro_sim_CheckedChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_portas();
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                //pes_portas();
            }
        }
        private void rb_portas_grlha_nao_CheckedChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_portas();
            }
        }
        private void rb_portas_vidro_s_CheckedChanged(object sender, EventArgs e)
       {
           if (b == true)
           {
               pes_portas();
           }
       }
        //_prodotos tabs
        private void i_prod_portas_Click(object sender, EventArgs e)
        {
            rb_portas.Checked = true;
        }
        private void i_prod_puxadores_Click(object sender, EventArgs e)
        {
            rb_puxadores.Checked = true;
        }
        private void i_prod_estores_Click(object sender, EventArgs e)
        {
            rb_estores.Checked = true;
        }
        private void i_prod_complemetos_Click(object sender, EventArgs e)
        {
            rb_complemetos.Checked = true;
        }
        private void i_prod_despesas_Click(object sender, EventArgs e)
        {
            //rb_assitencias.Checked = true;
        }
        private void i_prod_assitencias_Click(object sender, EventArgs e)
        {
            rb_assitencias.Checked = true;
        }
        //cb_familia
        private void cb_familia_complementos_SelectedIndexChanged(object sender, EventArgs e)
        {
            pes_complementos();
        }
        private void cb_familia_estores_SelectedIndexChanged(object sender, EventArgs e)
        {
            pes_estores();
        }
        private void cb_familia_puxadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            pes_puxadores();
        }
        private void cb_familia_assitencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            pes_assitencias();
        }
        //tb_pes pes por 1 ref
        private void tb_pes_portas_TextChanged(object sender, EventArgs e)
        {
                sql_string = "select * from portas where Referência LIKE'" + (tb_pes_portas.Text) + "%'";
                pes_tb();
        }
        private void tb_pes_puxadores_TextChanged(object sender, EventArgs e)
        {
                sql_string = "select * from puxadores where Referência LIKE'" + (tb_pes_puxadores.Text) + "'";
                pes_tb();
        }
        private void tb_pes_complemetos_TextChanged(object sender, EventArgs e)
        {
                sql_string = "select * from complemetos where Referência LIKE'" + (tb_pes_complemetos.Text) + "'";
                pes_tb();
        }
        private void tb_pes_estores_TextChanged(object sender, EventArgs e)
        {
                sql_string = "select * from estores where Referência LIKE'" + (tb_pes_estores.Text) + "'";
                pes_tb();
        }
        private void tb_pes_assitencias_TextChanged(object sender, EventArgs e)
        {
                sql_string = "select * from assistencias where Referência LIKE'" + (tb_pes_assitencias.Text) + "'";
                pes_tb();
        }
    }
}