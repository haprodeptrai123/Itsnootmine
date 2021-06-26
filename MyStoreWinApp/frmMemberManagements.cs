using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{  
    public partial class frmMemberManagements : Form
    {
        public bool isAdmin { get; set; }
        IMemberRepository memberRepository = new MemberRepository();
        //Create a data source
        BindingSource source;

        //----------------------------------------

        public frmMemberManagements()
        {
            InitializeComponent();
        }
        //----------------------------------------
        private void frmMemberManagements_Load(object sender, EventArgs e)
        {
            if (isAdmin == false)
            {
                btnDelete.Enabled = false;
                btnNew.Enabled = false;

                cboCity.Enabled = false;
                cboCountry.Enabled = false;
                txtEmail.Enabled = false;
                txtMemberID.Enabled = false;
                txtMemberName.Enabled = false;
                txtPassword.Enabled = false;
                btnDelete.Enabled = false;
                btnFind.Enabled = false;
                cboSearchCity.Enabled = false;
                cboSearchCountry.Enabled = false;
                dgvMemberList.CellDoubleClick += null;
            }
            else {
                btnDelete.Enabled = false;
                //Register this event to open the frmMemberDetail form that performs updating
                dgvMemberList.CellDoubleClick += DgvMemberList_CellDoubleClick; }
        }
        //----------------------------------------
        private void DgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails



            {
                Text = "Update member",
                InsertOrUpdate = true,
                MemberInfor = GetMemberObject(),
                MemberRepository = memberRepository
            };
            if (frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                //Set focus member updated
                source.Position = source.Count - 1;
            }
        }
        //Clear text on TextBoxes
        private void ClearText()
        {
            txtMemberID.Text = string.Empty;
            txtMemberName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            cboCountry.Text = string.Empty;
            cboCity.Text = string.Empty;
        }
        //-----------------------------------------------
        private MemberObject GetMemberObject()
        {
            MemberObject member = null;
            try
            {
                member = new MemberObject
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Password = txtPassword.Text,
                    Email = txtEmail.Text,
                    Country = cboCountry.Text,
                    City = cboCity.Text,
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member");
            }
            return member;
        }
        public void LoadMemberList()
        {
            var members = memberRepository.GetMembers();
         
            try
            {
                //The BindingSource omponent is designed to simplify
                //the process of binding controls to an underlying data source
                source = new BindingSource();
                source.DataSource = members.OrderByDescending(member=> member.MemberName);
                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                cboCountry.DataBindings.Clear();
                cboCity.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtEmail.DataBindings.Add("Text", source, "Email");
                cboCountry.DataBindings.Add("Text", source, "Country");
                cboCity.DataBindings.Add("Text", source, "City");


                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;
                if (isAdmin == false)
                {
                    if (members.Count() == 0)
                    {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                    }
                }
                else
                {
                    if (members.Count() == 0)
                    {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else
                    {
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }
        //------------------------------------------------------
        private void btnLoad_Click(object sender, EventArgs e)
        {
            
                LoadMemberList();
            
           
        }
        //-----------------------------------------------------
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails
            {
                Text = "Add member",
                InsertOrUpdate = false,
                MemberRepository = memberRepository
            };
            if (frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                //Set focus member inserted
                source.Position = source.Count - 1;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var member = GetMemberObject();
                memberRepository.DeleteMember(member.MemberID);
                LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a member");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadOneMember()
        {   
            MemberObject member = new MemberObject();
            var members = memberRepository.GetMembers();
            try
            {
                foreach (var i in members)
                {
                    //The BindingSource omponent is designed to simplify
                    //the process of binding controls to an underlying data source
                    if (i.MemberName.Equals(txtSearch.Text))
                    {
                        source = new BindingSource();


                        source.DataSource = memberRepository.GetMemberByID(i.MemberID);

                        txtMemberID.DataBindings.Clear();
                        txtMemberName.DataBindings.Clear();
                        txtPassword.DataBindings.Clear();
                        txtEmail.DataBindings.Clear();
                        cboCountry.DataBindings.Clear();
                        cboCity.DataBindings.Clear();

                        txtMemberID.DataBindings.Add("Text", source, "MemberID");
                        txtMemberName.DataBindings.Add("Text", source, "MemberName");
                        txtPassword.DataBindings.Add("Text", source, "Password");
                        txtEmail.DataBindings.Add("Text", source, "Email");
                        cboCountry.DataBindings.Add("Text", source, "Country");
                        cboCity.DataBindings.Add("Text", source, "City");


                        dgvMemberList.DataSource = null;
                        dgvMemberList.DataSource = source;
                        break;
                    }
                    else if(i.MemberID.ToString().Equals(txtSearch.Text))
                    {
                        source = new BindingSource();


                        source.DataSource = memberRepository.GetMemberByID(i.MemberID);

                        txtMemberID.DataBindings.Clear();
                        txtMemberName.DataBindings.Clear();
                        txtPassword.DataBindings.Clear();
                        txtEmail.DataBindings.Clear();
                        cboCountry.DataBindings.Clear();
                        cboCity.DataBindings.Clear();

                        txtMemberID.DataBindings.Add("Text", source, "MemberID");
                        txtMemberName.DataBindings.Add("Text", source, "MemberName");
                        txtPassword.DataBindings.Add("Text", source, "Password");
                        txtEmail.DataBindings.Add("Text", source, "Email");
                        cboCountry.DataBindings.Add("Text", source, "Country");
                        cboCity.DataBindings.Add("Text", source, "City");


                        dgvMemberList.DataSource = null;
                        dgvMemberList.DataSource = source;
                        break;
                    }
                     

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {   
            
            LoadOneMember();
        }
      
        private void FilterMember()
        {
          
            MemberObject member = new MemberObject();
            List<MemberObject> filterList = memberRepository.GetMemberByCityAndCountry(cboSearchCity.Text, cboSearchCountry.Text);
            try
            {

                if (filterList.Count == 0)
                {
                    MessageBox.Show("No member matched", "No result");
                }
                else if (filterList.Count != 0)
                {
                    source = new BindingSource();
                    source.DataSource = filterList.OrderByDescending(member => member.MemberName);
                    txtMemberID.DataBindings.Clear();
                    txtMemberName.DataBindings.Clear();
                    txtPassword.DataBindings.Clear();
                    txtEmail.DataBindings.Clear();
                    cboCountry.DataBindings.Clear();
                    cboCity.DataBindings.Clear();

                    txtMemberID.DataBindings.Add("Text", source, "MemberID");
                    txtMemberName.DataBindings.Add("Text", source, "MemberName");
                    txtPassword.DataBindings.Add("Text", source, "Password");
                    txtEmail.DataBindings.Add("Text", source, "Email");
                    cboCountry.DataBindings.Add("Text", source, "Country");
                    cboCity.DataBindings.Add("Text", source, "City");


                    dgvMemberList.DataSource = null;
                    dgvMemberList.DataSource = source;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

       

        private void btnFind_Click(object sender, EventArgs e)
        {
            FilterMember();
        }
        private void cboSearchCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            // searchCity= cboSearchCity.Text;
        }

        private void cboSearchCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            // searchCountry= cboSearchCountry.Text;

        }

    }
  
}
