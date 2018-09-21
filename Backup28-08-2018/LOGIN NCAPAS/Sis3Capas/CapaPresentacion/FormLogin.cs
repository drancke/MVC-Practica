using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CapaNegocio;
using System.Data.SqlClient;

namespace CapaPresentacion
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);


        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "Usuario") {
                txtuser.Text = "";
                txtuser.ForeColor = Color.LightGray;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if(txtuser.Text==""){
                txtuser.Text = "Usuario";
                txtuser.ForeColor = Color.Silver;
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "Contraseña")
            {
                txtpass.Text = "";
                txtpass.ForeColor = Color.LightGray;
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            if(txtpass.Text==""){
                txtpass.Text = "Contraseña";
                txtpass.ForeColor = Color.Silver;
                txtpass.UseSystemPasswordChar = false;
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            CNEmpleado objEmpleado = new CNEmpleado();
            SqlDataReader Loguear;
            objEmpleado.Usuario = txtuser.Text;
            objEmpleado.Contraseña = txtpass.Text;

            if (objEmpleado.Usuario == txtuser.Text)
            {
                lblErrorUsuario.Visible = false;

                if (objEmpleado.Contraseña == txtpass.Text)
                {
                    lblErrorPass.Visible = false;
                    Loguear = objEmpleado.IniciarSesion();

                    if (Loguear.Read() == true)
                    {
                        this.Hide();
                        FormPrincipal ObjFP = new FormPrincipal();
                        ObjFP.Show();
                    }
                    else
                    {
                        lblErrorLogin.Text = "Usuario o contraseña invalidas, intente de nuevo";
                        lblErrorLogin.Visible = true;
                        txtpass.Text = "";
                        txtpass_Leave(null, e);
                        txtuser.Focus();
                    }

                }
                else
                {
                    lblErrorPass.Text = objEmpleado.Contraseña;
                    lblErrorPass.Visible = true;
                }

            }
            else
            {
                lblErrorUsuario.Text = objEmpleado.Usuario;
                lblErrorUsuario.Visible = true;
            }  

        }
    }
}
