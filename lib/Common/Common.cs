using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Common
{

    public static class Constants {
        public static string BCAuthClient = "BCAuthClient";

    }
    public static class Common
    {
        public static string DateToStringFormat(this DateTime? value, string format)
        {
            if (!value.HasValue)
                return "";
            else
            {
                var datetime = value ?? DateTime.Now;
                var strYear = datetime.Year.ToString();

                format = format.Replace("yyyy", strYear);
                format = format.Replace("yy", strYear.Substring(strYear.Length - 2, 2));
                var strDate = datetime.ToString(format);

                return strDate;
            }
        }

        public static DataTable CreateDataTableFromModel<T>(this IEnumerable<T> list, ref List<string> ColumnName)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                ColumnName.Add(info.Name);
                dataTable.Columns.Add(new DataColumn(info.Name.ToLower(), Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static IEnumerable ObjectToIEnumerable(this object obj)
        {
            return new[] { obj };
        }
        public static string GetMailBodyTemplate()
        {
            string mailHeader = "";
            mailHeader = @"<html
                            xmlns:v='urn:schemas-microsoft-com:vml'
                            xmlns:o='urn:schemas-microsoft-com:office:office'
                            xmlns:w='urn:schemas-microsoft-com:office:word'
                            xmlns:m='http://schemas.microsoft.com/office/2004/12/omml'
                            xmlns = 'http://www.w3.org/TR/REC-html40' >
                            <head>
                                <meta http - equiv = 'Content-Type' content = 'text/html; charset=windows-874' />     
                                     <meta name = 'Generator' content = 'Microsoft Word 15 (filtered medium)' />        
                                 <style>        
                                            <!--  /* Font Definitions */  @font - face   { font - family:'Cordia New'; panose - 1:2 11 3 4 2 2 2 2 2 4; }
                                    @font - face   { font - family:'Cambria Math'; panose - 1:2 4 5 3 5 4 6 3 2 4; }
                                    @font - face   { font - family:Calibri; panose - 1:2 15 5 2 2 2 4 3 2 4; }
                                    @font - face   { font - family:Tahoma; panose - 1:2 11 6 4 3 5 4 4 2 4; }
                                    @font - face   { font - family:AP; panose - 1:0 0 5 0 0 0 0 0 0 0; }  /* Style Definitions */
                                    p.MsoNormal, li.MsoNormal, div.MsoNormal   { margin: 0cm; margin - bottom:.0001pt; font - size:11.0pt; font - family:'Calibri',sans - serif; }
                                    a: link, span.MsoHyperlink   {
                                        mso - style - priority:99; color:#0563C1;   text-decoration:underline;}  a:visited, span.MsoHyperlinkFollowed   {mso-style-priority:99;   color:#954F72;   text-decoration:underline;}  p.msonormal0, li.msonormal0, div.msonormal0   {mso-style-name:msonormal;   mso-margin-top-alt:auto;   margin-right:0cm;   mso-margin-bottom-alt:auto;   margin-left:0cm;   font-size:12.0pt;   font-family:'Tahoma',sans-serif;}  span.EmailStyle18   {mso-style-type:personal-compose;   font-family:'Calibri',sans-serif;   color:windowtext;}  .MsoChpDefault   {mso-style-type:export-only;   font-size:10.0pt;   font-family:'Calibri',sans-serif;}  @page WordSection1   {size:612.0pt 792.0pt;   margin:72.0pt 72.0pt 72.0pt 72.0pt;}  div.WordSection1   {page:WordSection1;}  -->
                                </style>
                        <style type='text/css'> .table,.tr,.td{font-family:AP;font-size:14px;border-style: solid;border-width: 1px;border-color: blue;vertical-align: top;padding:5px;border-collapse: collapse;}   
                        .receivername {font-weight: bold;color:darkblue;}
                        .tableClass {font-family:AP; padding:3px;}
                        </style>
                                <!--[if gte mso 9]><xml> <o:shapedefaults v:ext = 'edit' spidmax = '1026' /> </xml><![endif]-->      
                                      <!--[if gte mso 9]>       
                                           <xml>       
                                               <o:shapelayout v:ext = 'edit'> <o:idmap v:ext = 'edit' data = '1' /> </o:shapelayout>                 
                                                     </xml>                 
                                                 <![endif]-->                 
                                             </head>                 
                                             <body lang = 'EN-US' link = '#0563C1' vlink = '#954F72' style='font-family:AP;font-size:14px;' >                      
                                                      <div class='WordSection1'>
                                    <p class='MsoNormal'>
                                        <span lang = 'TH' >
                                            {bodycontent}
                                        </span>
                                    </p>
                                </div>
                        </div>
                        </br>
                        </br>
                        <div>
						<table border='0' cellspacing='0' cellpadding='0' style='font-family:AP;font-size:12px'>
						 <tr>
						 <td colspan='3' valign='top'><p style='color:#999999'><strong>CRM Revolution Team</strong><strong> </strong></p></td>
						 </tr>
						 <tr>
						 <td colspan='3' valign='top'></td>
						 </tr>
						 <tr>
						 <td width='140' valign='top'></td>
						 <td colspan='2' valign='top'></td>
						 </tr>
						 <tr>
						 <td width='140' rowspan='5' valign='top'><p><img width='100' height='120' src='https://happyrefund.apthai.com/datashare/maillogo/aplogo.png' alt='cid:image003.png@01D564DA.CEE30410' border='0' /></p>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div></td>
						 <td colspan='2' valign='top'><p><strong>AP (Thailand)&nbsp; Public Company Limited</strong> <br />
						 170/57 18th Fl. Ocean Tower 1 , Ratchadapisek Rd. <br />
						 Khongtoey Bangkok 10110 </p></td>
						 </tr>
						 <tr>
						 <td valign='top'><div align='left'>Sale Consult</div></td>
						 <td valign='top'>: &nbsp;02-261-2518 Ext.649, 477, 648, 647</td>
						 </tr>
						 <tr>
						 <td valign='top'><div align='left'>Account/FI Consult</div></td>
						 <td valign='top'>: &nbsp;02-261-2518 Ext.373, 647</td>
						 </tr>
						 <tr>
						 <td width='127' valign='top'><div align='left'>Email</div></td>
						 <td width='266' valign='top'>: &nbsp;<a href='mailto:crmsale@apthai.com'>crmsale@apthai.com</a></td>
						 </tr>
						</table>
                        </div>
                            </body>
                        </html>";
            return mailHeader.Replace("'", "\"");
        }
        public static List<T> ConvertDynamicList<T>(List<dynamic> dynamicList) where T : new()
        {
            List<T> list = new List<T>();

            foreach (var dyn in dynamicList)
            {
                T obj = new T();
                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    var value = dyn.GetType().GetProperty(prop.Name)?.GetValue(dyn, null);
                    if (value != null)
                    {
                        prop.SetValue(obj, value);
                    }
                }
                list.Add(obj);
            }

            return list;
        }
    }
}
