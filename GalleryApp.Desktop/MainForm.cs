using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GalleryApp.Data;
using GalleryApp.Data.Enums;
using GalleryApp.Data.Models;
using GalleryApp.Data.ViewModels;

namespace GalleryApp.Desktop
{
   
    //Yeni kayitta sayisal alanlar icin girilen degerin kontrolu ve textbox a sadece sayisal deger girilebilmesi
    //Yeni ve mevcut kayitta gerekli alanlar icin zorunlu olup olmadigini kontrolu.
    //update - delete - save button 

    public partial class MainForm : Form
    {

        //one for each table in DB. We keep them in lists, RAM.
        private List<Marka> Brands = new List<Marka>();
        private List<Model> Models = new List<Model>();
        private List<ModelDetail> ModelDetails = new List<ModelDetail>();

        private bool IsEditModeEnabled = false;

        public MainForm()
        {
            InitializeComponent();

            //to enable and disable "Editing Mode" to navigate records in DB.
            this.LoadData();
            this.BindData();
            this.DisableEditing();
        }

        private Database _db;
        private Database DB
        {
            get
            {
                if (_db == null)
                {
                    _db = new Database();
                }

                return _db;
            }
        }

        //Entity Framework receieves the data from DB and fills them out.
        private void LoadData()
        {
            //Get data from Database
            this.Models = DB.Model.ToList();
            this.Brands = DB.Marka.ToList();
            this.ModelDetails = DB.ModelDetail.ToList();
        }

        //loading List's and Enums data.
        private void BindData()
        {
            var brandsToBind = this.Brands.ConvertAll(brand => brand.Clone());
            brandsToBind.Insert(0, new Marka() { ID = 0, Name = "Select A Brand" });
            var bsBrands = new BindingSource();
            bsBrands.DataSource = brandsToBind;
            this.cbBrand.DisplayMember = "Name";
            this.cbBrand.ValueMember = "ID";
            this.cbBrand.DataSource = bsBrands.DataSource;

            //Copy the list
            var modelsToBind = this.Models.ConvertAll(model => model.Clone());
            //Add a item to first place of the list
            modelsToBind.Insert(0, new Model() { ID = 0, Name = "Select A Model" });

            //Create BindingSource 
            var bsModels = new BindingSource();
            bsModels.DataSource = modelsToBind;

            //Set Display and Value Member
            this.cbModel.DisplayMember = "Name";
            this.cbModel.ValueMember = "ID";

            //Assign the BindingSource to ComboBox
            this.cbModel.DataSource = bsModels.DataSource;

            var yearList = new List<string>() {"1989","1990","1991","1992","1993" ,"1994", "1995", "1996", "1997", "1998",
                "1999", "2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008",
                "2009", "2010", "2011", "2012", "2013", "2014", "2014", "2015", "2016", "2017", "2018",
                "2019", "2020" }.OrderByDescending(c => c).ToList();
            yearList.Insert(0, "Select A Year");
            this.cbYear.Items.AddRange(yearList.ToArray());

            string[] priceRange = new List<string>() { "Select A Price Range","20.000-50.000", "60.000-80000", "90.000-110.000",
                "120.000-160.000","170.000-200.000","250.000-300.000","350.000-400.000","450.000-500.000",
                "500.000-1.000.000"}.ToArray();
            this.cbPriceRange.Items.AddRange(priceRange);

            //fuel type to cb
            cbFuel.DataSource = Enum.GetValues(typeof(FuelTypeEnum));

            //body type to cb
            cbBody.DataSource = Enum.GetValues(typeof(BodyTypeEnum));

            //colour to cb
            cbColour.DataSource = Enum.GetValues(typeof(ColourEnum));

            //gear box to cb
            cbGearType.DataSource = Enum.GetValues(typeof(GearBoxTypeEnum));

            //status to cb
            cbStatus.DataSource = Enum.GetValues(typeof(StatusEnum));

            this.LoadGrid();
        }

        private void LoadGrid()
        {
            //Load grid  


            //filter model details screen
            var list = this.ModelDetails.Select(c => new GridModel()
            {
                ID = c.ID,
                markaID = this.Models.Where(a => a.ID == c.ModelID).Select(a => a.MarkaID).FirstOrDefault(),
                ModelName = this.Models.Where(a => a.ID == c.ModelID).Select(a => a.Name).FirstOrDefault(),
                MarkaName = "",
                BodyTypeName = Enum.GetName(typeof(BodyTypeEnum), c.BodyType),
                ColourName = Enum.GetName(typeof(ColourEnum), c.Colour),
                EngineName = c.Engine,
                FuelTypeName = Enum.GetName(typeof(FuelTypeEnum), c.FuelType),
                GearBoxTypeName = Enum.GetName(typeof(GearBoxTypeEnum), c.GearBoxType),
                Price = c.Price,
                Year = c.Year
            }).ToList();

            foreach (var item in list)
            {
                item.MarkaName = this.Brands.Where(a => a.ID == item.markaID).Select(a => a.Name).FirstOrDefault();
            }

            var grdlist = new BindingList<GridModel>(list);
            var grdsource = new BindingSource(grdlist, null);
            this.grdModelDetail.DataSource = grdsource;

        }
        //To clear when we add some record to DB 
        private void Clear()
        {

            this.cbBrand.DataSource = null;
            this.cbModel.DataSource = null;
            this.cbFuel.DataSource = null;
            this.cbBody.DataSource = null;
            this.cbColour.DataSource = null;
            this.cbGearType.DataSource = null;
            this.cbStatus.DataSource = null;


            cbPriceRange.Items.Clear();
            cbYear.Items.Clear();

            txtEngine.Clear();
            txtBoxPrice.Clear();
        }

        //First disables editing mode then cleans it and takes the data from db and loads it.
        private void ResetForm()
        {
            this.btnToggleEditing_Click(null, null);
            this.Clear();
            this.LoadData();
            this.BindData();

            this.IsEditModeEnabled = false;
        }

        //when we in edit mode, it hides some button that shouldn't be there while editing.
        private void EnableEditing()
        {
            this.IsEditModeEnabled = true;

            btnAddNew.Enabled = true;
            btnAddNew.Visible = true;

            btnDelete.Enabled = false;
            btnDelete.Visible = false;

            btnSave.Enabled = true;
            btnSave.Visible = true;

            txtBoxPrice.Enabled = true;
            txtBoxPrice.Visible = true;

            txtEngine.Enabled = true;

            grdModelDetail.Enabled = false;
            grdModelDetail.Visible = false;

            btnSearch.Enabled = false;
            btnSearch.Visible = false;

            cbPriceRange.Enabled = false;
        }

        //When edit mode activated some components will be disabled and some will enabled. This function is for this.
        private void DisableEditing()
        {
            this.IsEditModeEnabled = false;

            //in this state  can be see but cannot be used.
            btnAddNew.Enabled = false;
            //Invinsible mode is on.
            btnAddNew.Visible = false;

            btnDelete.Enabled = true;
            btnDelete.Visible = true;

            btnSave.Enabled = false;
            btnSave.Visible = false;

            txtBoxPrice.Enabled = false;
            //txtBoxPrice.Visible = false;

            txtEngine.Enabled = false;

            cbPriceRange.Enabled = true;

            grdModelDetail.Enabled = true;
            grdModelDetail.Visible = true;

            btnSearch.Enabled = true;
            btnSearch.Visible = true;
        }

        //this will add the entered data to modelDetails table
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            ModelDetail NewRecord = new ModelDetail();

            NewRecord.ModelID = (int)cbModel.SelectedValue;
            NewRecord.Engine = txtEngine.Text;

            int.TryParse(cbYear.SelectedItem.ToString(), out int year);
            NewRecord.Year = year;

            Enum.TryParse<StatusEnum>(cbStatus.SelectedValue.ToString(), out StatusEnum statusEnum);
            NewRecord.Status = statusEnum;

            Enum.TryParse<FuelTypeEnum>(cbFuel.SelectedValue.ToString(), out FuelTypeEnum fuelTypeEnum);
            NewRecord.FuelType = fuelTypeEnum;

            Enum.TryParse<ColourEnum>(cbColour.SelectedValue.ToString(), out ColourEnum colourEnum);
            NewRecord.Colour = colourEnum;

            Enum.TryParse<BodyTypeEnum>(cbBody.SelectedValue.ToString(), out BodyTypeEnum bodyTypeEnum);
            NewRecord.BodyType = bodyTypeEnum;

            Enum.TryParse<GearBoxTypeEnum>(cbGearType.SelectedValue.ToString(), out GearBoxTypeEnum gearBoxTypeEnum);
            NewRecord.GearBoxType = gearBoxTypeEnum;

            double.TryParse(txtBoxPrice.Text, out double price);
            NewRecord.Price = price;

            //Save the data to DB
            DB.ModelDetail.Add(NewRecord);
            DB.SaveChanges();

            //Reset Form
            this.ResetForm();
        }

        //states the editing mode by calling the specific funtion about it 
        private void btnToggleEditing_Click(object sender, EventArgs e)
        {
            if (this.IsEditModeEnabled)
            {
                this.DisableEditing();
            }
            else
            {
                this.EnableEditing();
            }
        }

        //Only user wants to refresh the grid data
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadData();
            this.LoadGrid();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {


            var query = DB.ModelDetail.AsQueryable();

            if (this.cbBrand.SelectedItem != null && (int)cbBrand.SelectedValue != 0)
            {
                var brandId = (int)cbBrand.SelectedValue;
                query = query.Where(a => a.Model.MarkaID == brandId).AsQueryable();
            }

            if (this.cbModel.SelectedItem != null && (int)cbModel.SelectedValue != 0)
            {
                var modelId = (int)cbModel.SelectedValue;
                query = query.Where(a => a.ModelID == modelId).AsQueryable();
            }

            if (this.cbYear.SelectedItem != null && cbYear.SelectedItem.ToString() != "Select A Year")
            {
                var yearId = int.Parse(cbYear.SelectedItem.ToString());
                query = query.Where(a => a.Year == yearId).AsQueryable();
            }

            if (this.cbPriceRange.SelectedItem != null && cbPriceRange.SelectedItem.ToString() != "Select A Price Range")
            {
                String[] prices = cbPriceRange.SelectedItem.ToString().Replace(".", "").Split('-');
                int.TryParse(prices[0], out int startPrice);
                int.TryParse(prices[1], out int endPrice);

                query = query.Where(a => a.Price >= startPrice && a.Price <= endPrice).AsQueryable();
            }

            if (this.cbFuel.SelectedItem != null && (FuelTypeEnum)cbFuel.SelectedValue != FuelTypeEnum.None)
            {
                var fuelId = (FuelTypeEnum)cbFuel.SelectedValue;
                query = query.Where(a => a.FuelType == fuelId).AsQueryable();
            }

            if (this.cbGearType.SelectedItem != null && (GearBoxTypeEnum)cbGearType.SelectedValue != GearBoxTypeEnum.None)
            {
                var gearId = (GearBoxTypeEnum)cbGearType.SelectedValue;
                query = query.Where(a => a.GearBoxType == gearId).AsQueryable();
            }

            if (this.cbColour.SelectedItem != null && (ColourEnum)cbColour.SelectedValue != ColourEnum.None)
            {
                var colourId = (ColourEnum)cbColour.SelectedValue;
                query = query.Where(a => a.Colour == colourId).AsQueryable();
            }

            if (this.cbBody.SelectedItem != null && (BodyTypeEnum)cbBody.SelectedValue != BodyTypeEnum.None)
            {
                var bodyId = (BodyTypeEnum)cbBody.SelectedValue;
                query = query.Where(a => a.BodyType == bodyId).AsQueryable();
            }

            if (this.cbStatus.SelectedItem != null && (StatusEnum)cbStatus.SelectedValue != StatusEnum.None)
            {
                var statusId = (StatusEnum)cbStatus.SelectedValue;
                query = query.Where(a => a.Status == statusId).AsQueryable();
            }


            this.ModelDetails = query.ToList();
            this.LoadGrid();
        }

        private void cbBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbBrand.SelectedItem != null)
            {
                var markaId = (int)cbBrand.SelectedValue;

                if (markaId != 0)
                {
                    this.Models = DB.Model.Where(a => a.MarkaID == markaId).ToList();

                    var modelsToBind = this.Models.ConvertAll(model => model.Clone());
                    modelsToBind.Insert(0, new Model() { ID = 0, Name = "Select A Model" });

                    var bsModels = new BindingSource();
                    bsModels.DataSource = modelsToBind;
                    this.cbModel.DataSource = bsModels.DataSource;
                }
            }
        }

        private void grdModelDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            grdModelDetail.Columns["ID"].Visible = false;
            grdModelDetail.Columns["markaID"].Visible = false;
            grdModelDetail.Columns["MarkaName"].DisplayIndex = 0;
            grdModelDetail.Columns["ModelName"].DisplayIndex = 1;
            grdModelDetail.Columns["Year"].DisplayIndex = 2;
            grdModelDetail.Columns["EngineName"].DisplayIndex = 3;
            grdModelDetail.Columns["FuelTypeName"].DisplayIndex = 4;
            grdModelDetail.Columns["GearBoxTypeName"].DisplayIndex = 5;
            grdModelDetail.Columns["ColourName"].DisplayIndex = 6;
            grdModelDetail.Columns["BodyTypeName"].DisplayIndex = 7;
            grdModelDetail.Columns["Price"].DisplayIndex = 8;
            grdModelDetail.Columns["ID"].DisplayIndex = 9;

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(grdModelDetail.CurrentCell.RowIndex != null){
                var selectedRow = grdModelDetail.CurrentCell.RowIndex;

                if(selectedRow != null){
                    if(grdModelDetail.Rows[selectedRow].Cells[0] != null){
                        var idVal = grdModelDetail.Rows[selectedRow].Cells[0].Value;

                        int.TryParse(idVal.ToString(), out int id);

                        var rec = DB.ModelDetail.Where(a=> a.ID == id).FirstOrDefault();
                        if(rec != null){
                            DB.ModelDetail.Remove(rec);
                            DB.SaveChanges();

                            this.LoadData();
                            this.LoadGrid();
                        }
                    }
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }

    //16/01/2020 12:44 AM This app was finished.
}
