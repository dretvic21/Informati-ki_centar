using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IcKatalog.Models;
using System.Drawing;
using System.Collections.Specialized;

namespace IcKatalog
{
    public partial class Katalog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpRequest q = Request;
            NameValueCollection n = q.QueryString;
            int artid=0;
            if (n.HasKeys())
            {
                string k = n.GetKey(0);
                if (k == "artid")
                {
                    string v = n.Get(0);
                    if (int.TryParse(v, out artid) == true)
                    {
                        if (artid != 0)
                        {
                            if (OdaberiArtikal(artid))
                            { 
                                OtvoriZatvoriGrid(false); 
                            }
                        }
                        else OtvoriZatvoriGrid(true);
                    }
                    else OtvoriZatvoriGrid(true);
                }
                else
                {
                    OtvoriZatvoriGrid(true);
                }
            }
            if (!this.IsPostBack)
            {
                this.populateNode();
            }
        }
        protected void OtvoriZatvoriGrid(bool sw_otvori)
        {
            if (sw_otvori == true)
            {
                GridView1.Visible = true;
                FormView1.Visible = false;
            }
            else
            {
                GridView1.Visible = false;
                FormView1.Visible = true;
            }
        }
        protected bool OdaberiArtikal(int artid)
        {
            var _db = new IcKatalog.Models.radnaEntities();
            var sqlartikal = from
                                 c1 in _db.artikal
                             join
                                 c2 in _db.zaliha on c1.artid equals c2.artid 
                             join
                                c3 in _db.skla on c2.skid equals c3.skid
                             where c1.artid == artid
                             select new { c1.artid,c1.sifra, c1.opis, c1.jm };
            GridView1.DataSource = sqlartikal.ToList();
            GridView1.DataBind();
            
            return true;
        }
        protected void populateNode()
        {

            var _db = new IcKatalog.Models.radnaEntities();
            //.Where(k => k.gartid1 == 0 || k.gartid1 == null)
            foreach (var t in _db.gartikal.OrderBy(k => k.sifra).Where(k => k.gartid1 == 0 || k.gartid1 == null))
            {
                TreeNode NewNode = new TreeNode(t.sifra.Trim() + " " + t.opis, t.gartid.ToString().Trim());
                //                  NewNode.PopulateOnDemand = true;
                NewNode.SelectAction = TreeNodeSelectAction.Expand;
                populateSubNote(_db, NewNode, t.gartid);
                TreeView1.Nodes.Add(NewNode);
            }
            TreeView1.CollapseAll();
            return;
        }
        void populateSubNote(radnaEntities _db, TreeNode node, int lgarid)
        {
            foreach (var t in _db.gartikal.OrderBy(k => k.sifra).Where(k => k.gartid1 == lgarid))
            {
                TreeNode NewNode = new TreeNode(t.sifra.Trim() + " " + t.opis, t.gartid.ToString().Trim());
                //                NewNode.PopulateOnDemand = true;
                NewNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                populateSubNote(_db, NewNode, t.gartid);
                node.ChildNodes.Add(NewNode);
            }
            return;
        }

        protected void changegrid(object sender, EventArgs e)
        {
            int garid;
            var _db = new IcKatalog.Models.radnaEntities();
           
            Int32.TryParse(TreeView1.SelectedNode.Value,out garid);
            var sqlartikal= from 
                                c1 in _db.artikal join 
                                c2 in _db.grupeartikli on c1.artid equals c2.artid
                            orderby(c1.sifra)
                            where c2.gartid==garid
                            select new {c1.artid, c1.sifra, c1.opis,c1.jm };
            GridView1.DataSource = sqlartikal.ToList();
            GridView1.DataBind();
        }

        protected void Gridview1_sort(object sender, GridViewSortEventArgs e)
        {
            int garid;
            var _db = new IcKatalog.Models.radnaEntities();
                        Int32.TryParse(TreeView1.SelectedNode.Value, out garid);
            var sqlartikal = from
                         c1 in _db.artikal
                         join
                             c2 in _db.grupeartikli on c1.artid equals c2.artid
                         orderby (c1.sifra)
                         where c2.gartid == garid
                         select new { c1.sifra, c1.opis, c1.jm };
            if (e.SortExpression == "jm")
            {
                sqlartikal = from
                         c1 in _db.artikal
                             join
                                 c2 in _db.grupeartikli on c1.artid equals c2.artid
                             orderby (c1.jm)
                             where c2.gartid == garid
                             select new { c1.sifra, c1.opis, c1.jm };
           
            } else
            if (e.SortExpression == "opis")
            {
               sqlartikal = from
                         c1 in _db.artikal
                                 join
                                     c2 in _db.grupeartikli on c1.artid equals c2.artid
                                 orderby (c1.opis)
                                 where c2.gartid == garid
                                 select new { c1.sifra, c1.opis, c1.jm };

            }else
            {
               sqlartikal = from
                            c1 in _db.artikal
                                 join
                                     c2 in _db.grupeartikli on c1.artid equals c2.artid
                                 orderby (c1.sifra)
                                 where c2.gartid == garid
                                 select new { c1.sifra, c1.opis, c1.jm };

            }
            GridView1.DataSource = sqlartikal.ToList();
            GridView1.DataBind();

        }

        protected void gridview1_onselect(object sender, EventArgs e)
        {
/*            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowIndex == GridView1.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
 */
        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                e.Row.TabIndex = -1;
                e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
                e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
                e.Row.Attributes["onselectstart"] = "javascript:return false;";
            }
        }

       
    }

}