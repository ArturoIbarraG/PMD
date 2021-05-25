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


Partial Public Class Cotizaciones
    
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
    '''Control btnSolicitarAutorizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnSolicitarAutorizacion As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control btnAgregarCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregarCotizacion As Global.System.Web.UI.WebControls.Button
    
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
    '''Control btnEliminarCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnEliminarCotizacion As Global.System.Web.UI.WebControls.Button
    
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
    '''Control updDocumentosCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents updDocumentosCotizacion As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control rptDocumentos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rptDocumentos As Global.System.Web.UI.WebControls.Repeater
    
    '''<summary>
    '''Control btnGuardarSeleccionDocumento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnGuardarSeleccionDocumento As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control updNuevaCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents updNuevaCotizacion As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control hdnIdCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdnIdCotizacion As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control hdnIdCotizacionDetalle.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdnIdCotizacionDetalle As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control lblInformacionCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblInformacionCotizacion As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control ddlTipoProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlTipoProducto As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control tituloNombre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents tituloNombre As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control txtProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtProducto As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control pnlInfoProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlInfoProducto As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control txtCantidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCantidad As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control ddlUnidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlUnidad As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control pnlInfoServicio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlInfoServicio As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Control txtFechaInicio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaInicio As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control txtFechaInicio_CalendarExtender.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaInicio_CalendarExtender As Global.AjaxControlToolkit.CalendarExtender
    
    '''<summary>
    '''Control txtFechaTerminacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaTerminacion As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control txtFechaTerminacion_CalendarExtender.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaTerminacion_CalendarExtender As Global.AjaxControlToolkit.CalendarExtender
    
    '''<summary>
    '''Control txtVigencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtVigencia As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control ddlAlmacen.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlAlmacen As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control txtJustificacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtJustificacion As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control txtDescripcionProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDescripcionProducto As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control ddlTipoEspecificacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlTipoEspecificacion As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control txtEspecificacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEspecificacion As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control btnAgregar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control grdEspecificaciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents grdEspecificaciones As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control afUploadCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents afUploadCotizacion As Global.AjaxControlToolkit.AsyncFileUpload
    
    '''<summary>
    '''Control btnRecargaArchivos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnRecargaArchivos As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control grdArchivosCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents grdArchivosCotizacion As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control btnGuardarCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnGuardarCotizacion As Global.System.Web.UI.WebControls.Button
    
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
