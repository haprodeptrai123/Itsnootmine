using BusinessObject;
using DataAccess.Repository;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        private MemberRepository memberRepository= new MemberRepository();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
                string json = string.Empty;

                // read json string from file
                using (StreamReader reader = new StreamReader("appsettings.json"))
                {
                    json = reader.ReadToEnd();
                }

                JavaScriptSerializer jss = new JavaScriptSerializer();

                // convert json string to dynamic type
                var obj = jss.Deserialize<dynamic>(json);

                // get contents
 
                var admin = new MemberObject
                {
                    Email = obj["DefaultAccount"]["Email"],
                    Password = obj["DefaultAccount"]["password"]
                };

            

            // add employees to richtextbox
        


        var members = memberRepository.GetMembers();
            bool canLog=false;
                foreach (var i in members)
                {
                    if (i.MemberName.Equals(txtUserName.Text) && i.Password.Equals(txtPassword.Text))
                    {
                        frmMemberManagements frm = new frmMemberManagements()
                        {
                            isAdmin = false
                        };
                        frm.ShowDialog();
                    canLog = true;
                        this.Close();


                    }
                    else if (admin.Email.Equals(txtUserName.Text) && admin.Password.Equals(txtPassword.Text))
                    {
                        frmMemberManagements frm = new frmMemberManagements()
                        {
                            isAdmin = true
                        };
                        frm.ShowDialog();

                        this.Close();


                    }


                }
            if (canLog == false)
            {
                MessageBox.Show("Can not find member", "Error"); 
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

    }
}
