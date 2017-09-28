using FFLeastResistancePath.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FFLeastResistancePath
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnGetOutput_Click(object sender, EventArgs e)
        {
            ClearData();
            string ext = System.IO.Path.GetExtension(fileInput.PostedFile.FileName);
            if(!fileInput.HasFile)
            {
                divMessage.InnerHtml = "Please select file";
                fileInput.Focus();

            }
            else if(ext != ".txt")
            {
                divMessage.InnerHtml = "Invalid file extension. Only .txt file are allowed";
                fileInput.Focus();
            }
            else
            {
                string filePath = Server.MapPath("~/InputFiles/Input_" + DateTime.Now.Ticks + ".txt");
                fileInput.SaveAs(filePath);
                LeastResistancePath objLeastResistancePath = new LeastResistancePath();
                PathResult result = new PathResult();
                result = objLeastResistancePath.Find(filePath);
                if(result.HasErrors == false)
                {
                    string lineBreak = "<br />";
                    divInput.InnerHtml = String.Format("Input{0}{1}", lineBreak, File.ReadAllText(filePath).Replace("\n", lineBreak));
                    divOutput.InnerHtml = String.Format("Output{0}{1}{0}{2}{0}{3}", lineBreak, result.SuccessResult, result.Resistance, result.LeastPath);
                } else
                {
                    divMessage.InnerHtml = result.ErrorMessage;
                }
            }
        }
        private void ClearData()
        {
            divMessage.InnerHtml = "";
            divInput.InnerHtml = "";
            divOutput.InnerHtml = "";
        }
    }
}