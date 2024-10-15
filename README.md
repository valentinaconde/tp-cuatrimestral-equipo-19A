# TP CUATRIMESTRAL - GRUPO 19 A

Realizado por Valentina Conde, Les Yparraguirre y Milagros Dubuis.

• Comando para clonar repo:   

    git clone https://github.com/valentinaconde/tp-cuatrimestral-equipo-19A.git

# Descripcion del proyecto:<br/>
## Propuesta Comercio<br/>
Se requiere una aplicación web para administrar las compras y ventas de un negocio multipropósito.<br/>
El sistema debe administrar clientes, proveedores, productos y registro de ventas y compras.<br/>
Los productos van a estar discriminados por marcas y tipos y/o categorias (también administrables), además, estarán asociados a uno o más proveedores.<br/>
Deben contar con un stock actual y un stock mínimo a tener en cuenta para proyectar las compras. El usuario podrá en todo momento dar de alta nuevas marcas, tipos, productos, proveedores y realizar nuevas asociaciones o modificar las existentes.<br/>
Cuando se ingresa una compra, se debe registrar en qué proveedor se compró y qué; de ésta
manera se deben generar las líneas de stock correspondientes, el "stock actual" y el registro de los precios de compra.<br/>
Para vender se debe contar con un formulario en el que se asigna el cliente (que debe estar<br/>
registrado en el sistema para poder comprar) y los productos con sus correspondientes cantidades, precios unitarios, parciales y finales. El sistema debe validar las cantidades de stock solicitadas <br/>
con el fin de no vender lo que no se tiene. Una vez realizada la venta se deben realizar los descuentos de stock correspondientes y generar un reporte con la factura<br/>
(se requiere un algoritmo de generación de número de factura único) para ser impresa.<br/>
Los precios se deben manejar con porcentajes. Cada producto tendrá un porcentaje de ganancia asignado y al momento de vender se tomará el precio de compra más reciente y aplicará el porcentaje de ganancia para calcular el precio de venta.<br/>
El sistema debe manejar seguridad (ingreso con usuario y  contraseña), con un perfil vendedor que registra ventas y un perfil administrador que puede hacer todo.<br/>

