<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.UI.MobileControls.MobilePage"
ContentType="text/html"
 %>
<%@ Register tagprefix="mobile" Namespace="System.Web.Mobile" Assembly="System.Web.Mobile" %>
<script runat="server">
public void MyEventHandler(Object source, ListCommandEventArgs e)
{
    Selection.Text = "You selected " + e.ListItem.Text;
    ActiveForm = SecondForm;

    switch (e.ListItem.Text)
    {
        case "French":
            Selection.Text = "Bonjour le monde";
            break;

        case "German":
            Selection.Text = "Hallo Welt";
            break;

        case "Italian":
            Selection.Text = "Ciao il mondo";
            break;

        case "Norwegian":
            Selection.Text = "Hei verden";
            break;

        case "Portuguese":
            Selection.Text = "Oi mundo";
            break;

        default:
            Selection.Text = "Hello World";
            break;
    }
}
</script>
<mobile:Form id="ListStuff" runat="server" 
      BackColor="White" ForeColor="#bb7023">
  <mobile:Label runat=server id="label">
    Pick a Language!
  </mobile:Label>
  <mobile:List runat=server id="ListMe" 
      OnItemCommand="MyEventHandler">
    <item Text="English" />
    <item Text="French" />
    <item Text="German" />
    <item Text="Italian" />
    <item Text="Norwegian" />
    <item Text="Portuguese" /> 
  </mobile:List>
</mobile:Form>

<mobile:Form id="SecondForm" runat="server"
    BackColor="White" ForeColor="Green">
  <mobile:Label runat=server>
     Your "Hello World"  Translation
  </mobile:Label>
  <mobile:Label runat=server 
    id="Selection"></mobile:Label>
  <mobile:Link runat=server id="GoBack" 
    NavigateURL="#ListStuff">back</mobile:Link>
</mobile:Form>