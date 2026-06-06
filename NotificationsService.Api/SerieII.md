# SERIE II

## 1. Uso de colas en infraestructura de aplicaciones

La estructura de datos **cola** funciona bajo el principio **FIFO** (*First In, First Out*), es decir, el primer elemento en entrar es el primero en salir. A nivel de infraestructura de aplicaciones, este concepto permite organizar solicitudes, pedidos o tareas que no siempre deben procesarse directamente en el mismo momento en que el usuario las envia.

En el proyecto final de **Super Bodega**, el supermercado necesita una plataforma moderna para ventas en linea, ya que su sistema anterior presenta caidas constantes y perdida de pedidos. En este escenario, una cola ayuda a que la API E-Commerce pueda recibir muchas compras simultaneas sin saturar inmediatamente la logica de negocio, la base de datos o el servicio de notificaciones.

Cuando un cliente realiza una compra desde el catalogo en linea, la aplicacion puede registrar la solicitud inicial y enviar un mensaje a una cola con la informacion del pedido, los productos, cantidades y datos del cliente. Luego, uno o varios servicios consumidores procesan esos mensajes de forma ordenada para validar existencias, crear la venta, actualizar inventario y generar notificaciones por correo electronico.

Este enfoque contribuye a la escalabilidad porque separa la recepcion del pedido del procesamiento interno. La API puede seguir atendiendo usuarios mientras los consumidores procesan pedidos en segundo plano. Si en algun momento ingresan muchas compras al mismo tiempo, los mensajes quedan en cola y se atienden conforme los servicios esten disponibles, evitando perdidas de informacion y reduciendo el riesgo de caidas.