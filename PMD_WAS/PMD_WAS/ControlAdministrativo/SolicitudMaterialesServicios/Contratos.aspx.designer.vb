'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class Contratos
    
    '''<summary>
    '''Control updRequisiciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents updRequisiciones As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control lblInformacionRequisicion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblInformacionRequisicion As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control hdnAdmon.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdnAdmon As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control hdnAnio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdnAnio As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control hdnMes.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdnMes As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control ddlSecretaria.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlSecretaria As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control ddlDireccion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlDireccion As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control ddlActividad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlActividad As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control ddlSubActividad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlSubActividad As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control pnlAltaSolicitud.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlAltaSolicitud As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control ddlContrato.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlContrato As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control ddlRequerimiento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlRequerimiento As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control txtCantidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCantidad As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control btnAgregarRequerimiento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregarRequerimiento As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control lblPresupuestoAutorizado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPresupuestoAutorizado As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lblPresupuestoUtilizado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPresupuestoUtilizado As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lblPresupuestoDisponible.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPresupuestoDisponible As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lnqRequisiciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnqRequisiciones As Global.System.Web.UI.WebControls.LinqDataSource
    
    '''<summary>
    '''Control grdRequisiciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents grdRequisiciones As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control grdOrdenesSurtido.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents grdOrdenesSurtido As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control lblTotalUtilizado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTotalUtilizado As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control updEnviar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents updEnviar As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control ddlAlmacen.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlAlmacen As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control btnEnviaOrdenesSurtido.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnEnviaOrdenesSurtido As Global.System.Web.UI.WebControls.Button
End Class
