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


Partial Public Class AutorizacionLineas
    
    '''<summary>
    '''Control updAutorizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents updAutorizacion As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control ddlAnio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlAnio As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control ddlSecretaria.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlSecretaria As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lblSecretaria.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblSecretaria As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control ddlDependencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlDependencia As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control lblDependencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblDependencia As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control ddlActividad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlActividad As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control Label1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label1 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lblPresupuestoLeyenda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPresupuestoLeyenda As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control rptPresupuesto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rptPresupuesto As Global.System.Web.UI.WebControls.Repeater
    
    '''<summary>
    '''Control lblFooterPresupuestado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFooterPresupuestado As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lblFooterCapturado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFooterCapturado As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control lblFooterDiferencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblFooterDiferencia As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control btnActualizar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnActualizar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control grdPresupuestos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents grdPresupuestos As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control lblTotal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTotal As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control hdnLinea.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdnLinea As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control updModalReducir.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents updModalReducir As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control txtReducirSueldo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtReducirSueldo As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control txtReducirMateriales.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtReducirMateriales As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control txtReducirVehiculos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtReducirVehiculos As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control txtComentariosReducir.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtComentariosReducir As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control btnGuardaReducir.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnGuardaReducir As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control updRechazar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents updRechazar As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control txtComentariosRechazar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtComentariosRechazar As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control hdnSueldoRechazar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdnSueldoRechazar As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control hdnMaterialesRechazar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdnMaterialesRechazar As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control hdnVehiculosRechazar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hdnVehiculosRechazar As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Control btnRechazar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnRechazar As Global.System.Web.UI.WebControls.Button
End Class
