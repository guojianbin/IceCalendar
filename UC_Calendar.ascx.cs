using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// 日期控件
/// 作者：郭建斌
/// 日期：2013
/// 联系：632628489@qq.com
/// </summary>
public partial class Ice_UC_Calendar : System.Web.UI.UserControl
{
    protected override void OnPreRender(EventArgs e)
    {
        SetJavascript("Calendar.js", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //
    }

    string _onChangeEvent;
    public string OnChangeEvent
    {
        set
        {
            _onChangeEvent = value;
            txtDateTime.Attributes.Add("onchange", value);
        }

    }

    /// <summary>
    /// 日期值
    /// </summary>
    public int SelectDateValue
    {
        get
        {
            if (txtDateTime.Value.Length > 0)
            {
                string[] nums = txtDateTime.Value.Split('-');
                string result = "";
                if (nums.Length > 0)
                {
                    foreach (string item in nums)
                    {
                        result += item;
                    }
                }
                return Convert.ToInt32(result);
            }
            else
            {
                return 0;
            }

        }
    }
    /// <summary>
    /// 年份
    /// </summary>
    public string Year
    {
        get
        {
            if (txtDateTime.Value.Length > 0)
            {
                string[] nums = txtDateTime.Value.Split('-');
                if (nums.Length > 0)
                {
                    return nums[0];
                }
                return "";
            }
            else
            {
                return "";
            }

        }
    }
    /// <summary>
    /// 月份
    /// </summary>
    public string Month
    {
        get
        {
            if (txtDateTime.Value.Length > 0)
            {
                string[] nums = txtDateTime.Value.Split('-');
                if (nums.Length > 1)
                {
                    return nums[1];
                }
                return "";
            }
            else
            {
                return "";
            }

        }
    }
    /// <summary>
    /// 天数
    /// </summary>
    public string Day
    {
        get
        {
            if (txtDateTime.Value.Length > 0)
            {
                string[] nums = txtDateTime.Value.Split('-');
                if (nums.Length > 2)
                {
                    return nums[2];
                }
                return "";
            }
            else
            {
                return "";
            }

        }
    }
    /// <summary>
    /// 日期文本
    /// </summary>
    public string SelectDateText
    {
        get
        {
            if (txtDateTime.Value.Length > 0)
            {
                return txtDateTime.Value;
            }
            else
            {
                return "";
            }

        }
        set
        {
            txtDateTime.Value = value;
        }
    }
    /// <summary>
    /// 日期
    /// </summary>
    public DateTime SelectDate
    {
        get
        {
            if (txtDateTime.Value.Length > 0)
            {
                return Convert.ToDateTime(txtDateTime.Value);
            }
            else
            {
                return new DateTime();
            }

        }
    }
    /// <summary>
    /// 当天最小日期
    /// </summary>
    public string SelectDateMinText
    {
        get
        {
            if (txtDateTime.Value.Length > 0)
            {
                return txtDateTime.Value + " 00:00:00";
            }
            else
            {
                return "";
            }

        }
    }
    /// <summary>
    /// 当天最大日期
    /// </summary>
    public string SelectDateMaxText
    {
        get
        {
            if (txtDateTime.Value.Length > 0)
            {
                return txtDateTime.Value + " 23:59:59";
            }
            else
            {
                return "";
            }

        }
    }
    /// <summary>
    /// 当前日期月份最后一天
    /// </summary>
    public int LastDayOfMonth
    {
        get
        {
            if (txtDateTime.Value.Length > 0)
            {
                return GetLastDay(Convert.ToInt32(Year), Convert.ToInt32(Month));
            }
            else
            {
                return 1;
            }

        }
    }

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool VisibleOfCalendar
    {
        get { return txtDateTime.Visible; }
        set { txtDateTime.Visible = value; }
    }

    /// <summary>
    /// 获取某年某月最后一天
    /// </summary>
    /// <param name="Year"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    private int GetLastDay(int Year, int month)
    {
        int endDay = 1;
        switch (month)
        {
            case 2:
                if ((Year % 4 == 0 && Year % 100 != 0) || Year % 400 == 0)
                {
                    endDay = 29;
                }
                else
                {
                    endDay = 28;
                }
                break;
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                endDay = 31;
                break;

            case 4:
            case 6:
            case 9:
            case 11:
                endDay = 30;
                break;
        }
        return endDay;
    }

    /// <summary>
    /// 设置网页Javascript脚本文件
    /// </summary>
    /// <param name="jsFilePath"></param>
    /// <param name="isInHead"></param>
    public static void SetJavascript(string jsFilePath, bool isInHead)
    {
        if (isInHead)
        {
            Page currentHandler = HttpContext.Current.CurrentHandler as Page;
            HtmlGenericControl child = new HtmlGenericControl("script");
            child.ID = "calendaScript";
            child.Attributes.Add("type", "text/javascript");
            child.Attributes.Add("language", "javascript");
            child.Attributes.Add("src", jsFilePath);
            HtmlGenericControl childExists = currentHandler.Header.FindControl("calendaScript") as HtmlGenericControl;
            if (childExists == null)
            {
                currentHandler.Header.Controls.Add(child);
            }

        }
        else
        {
            string str = string.Format("\n<script  type=\"text/javascript\" src=\"{0}\">\n</script>\n", jsFilePath);
            ((Page)HttpContext.Current.Handler).ClientScript.RegisterStartupScript(Type.GetType("System.String"), Guid.NewGuid().ToString(), str);
        }
    }

}
