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


Partial Public Class CotizacionAutorizacion
    
    '''<summary>
    '''Control updRequisiciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents updRequisiciones As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control hdnClaveSubmisionID.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdnClaveSubmisionID As Global.System.Web.UI.WebControls.HiddenField
    
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
    '''Control pnlAgregarCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlAgregarCotizacion As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control btnAutorizarCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAutorizarCotizacion As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control chkTodos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents chkTodos As Global.System.Web.UI.WebControls.CheckBox
    
    '''<summary>
    '''Control hdnIdCotizacionEliminar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdnIdCotizacionEliminar As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control grdCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents grdCotizacion As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control lblInfoEstatusCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblInfoEstatusCotizacion As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control updRechazarSolicitud.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents updRechazarSolicitud As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control ddlMotivoRechazo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlMotivoRechazo As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control txtComentariosRechazo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtComentariosRechazo As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control btnRechazarCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnRechazarCotizacion As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control ucComentariosCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ucComentariosCotizacion As Global.PMD_WAS.ucAgregaComentarioCotizacion
    
    '''<summary>
    '''Control ucCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ucCotizacion As Global.PMD_WAS.ucVistaCotizacion
End Class
