using MaterialSkin.Controls;
using StufoodSystem_ADMIN.Controllers;
using StufoodSystem_ADMIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StufoodSystem_ADMIN.Views
{
    public partial class IngredientCollectionCheckbox : MaterialSkin.Controls.MaterialForm
    {
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private List<Ingredient> providedList;
        private MaterialSkin.Controls.MaterialButton btnSaveSelected;
        private CheckedListBox checkedListBox1;
        private Supplier supplier;
        private List<String> resultList;

        public IngredientCollectionCheckbox(Supplier supplier)
        {
            InitializeComponent();
            this.providedList = IngredientController.GetAllIgredient();
            this.supplier = supplier;

            List<String> temp = new List<String>();
            foreach (Ingredient ingredient in supplier.ingredientProvided)
            {
                temp.Add(ingredient.ingredientID);
            }
            this.resultList = temp;

            InitializeCheckBoxes();
        }

        private void InitializeComponent()
        {
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.btnSaveSelected = new MaterialSkin.Controls.MaterialButton();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(16, 75);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(143, 19);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = "Ingredient collection";
            // 
            // btnSaveSelected
            // 
            this.btnSaveSelected.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSaveSelected.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSaveSelected.Depth = 0;
            this.btnSaveSelected.HighEmphasis = true;
            this.btnSaveSelected.Icon = null;
            this.btnSaveSelected.Location = new System.Drawing.Point(599, 387);
            this.btnSaveSelected.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSaveSelected.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSaveSelected.Name = "btnSaveSelected";
            this.btnSaveSelected.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSaveSelected.Size = new System.Drawing.Size(64, 36);
            this.btnSaveSelected.TabIndex = 0;
            this.btnSaveSelected.Text = "Save";
            this.btnSaveSelected.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSaveSelected.UseAccentColor = false;
            this.btnSaveSelected.UseVisualStyleBackColor = true;
            this.btnSaveSelected.Click += new System.EventHandler(this.btnSaveSelected_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(6, 107);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(657, 244);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // IngredientCollectionCheckbox
            // 
            this.ClientSize = new System.Drawing.Size(680, 434);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.btnSaveSelected);
            this.Controls.Add(this.materialLabel1);
            this.Name = "IngredientCollectionCheckbox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private void InitializeCheckBoxes()
        {
            foreach (Ingredient item in providedList)
            {
                // Add checkboxes to the CheckListBox
                bool isChecked = resultList.Contains(item.ingredientID);
                checkedListBox1.Items.Add(item.ingredientID + ": " + item.ingredientName, isChecked);
            }
        }
        private void btnSaveSelected_Click(object sender, EventArgs e)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach(String ingredientID in resultList)
            {
                Ingredient ingredient = IngredientController.GetIngredientByID(ingredientID);
                ingredients.Add(ingredient);
            }
            supplier.ingredientProvided = ingredients;
            SupplierController.UpdateSupplier(supplier.supplierId, supplier);

            this.Close();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Handle checkbox state change
            if (e.NewValue == CheckState.Checked)
            {
                // Item is checked, add to resultList
                string selectedItem = checkedListBox1.Items[e.Index].ToString();

                int indexOfColon = selectedItem.IndexOf(':');
                if (indexOfColon != -1)
                {
                    // Copy the part of the string before the colon
                    string result = selectedItem.Substring(0, indexOfColon);
                    if (!resultList.Contains(result)) resultList.Add(result);
                }

            }
            else
            {
                // Item is unchecked, remove from resultList
                string selectedItem = checkedListBox1.Items[e.Index].ToString();

                int indexOfColon = selectedItem.IndexOf(':');
                if (indexOfColon != -1)
                {
                    // Copy the part of the string before the colon
                    string result = selectedItem.Substring(0, indexOfColon);
                    resultList.Remove(result);
                }

            }
        }
    }


}
