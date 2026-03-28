
# TCP PowerMonitor: Simulador de Telemetría Industrial en .NET

Este proyecto es una **Prueba de Concepto (PoC)** diseñada para simular la captura y procesamiento de datos de medidores eléctricos en tiempo real mediante el protocolo **TCP/IP**. Está orientado a demostrar la capacidad de manejar comunicaciones de bajo nivel, asincronismo y resiliencia en sistemas críticos de energía.

## 🚀 Características Técnicas

- **Comunicación Persistente:** Implementación de un flujo de datos continuo sobre el protocolo TCP utilizando `NetworkStream`.
- **Arquitectura Resiliente:** El cliente posee una lógica de **reconexión automática** que se activa ante fallos en el servidor o caídas de red, asegurando la continuidad del servicio.
- **Procesamiento Asincrónico:** Uso de métodos asíncronos como `AcceptTcpClientAsync` y `ReadAsync` para evitar el bloqueo del hilo principal y mejorar el rendimiento.
- **Parsing de Protocolo de Aplicación:** Implementación de una estructura de mensajes personalizada (basada en delimitadores `|` y `:`) para traducir bytes en métricas eléctricas procesables.

## 🛠️ Estructura del Sistema

### 1. El Servidor (Central de Monitoreo)

Actúa como el concentrador de datos. Utiliza `TcpListener` para abrir un puerto de escucha y aceptar conexiones entrantes.

- **Gestión de Conexiones:** Al recibir una conexión, se genera un objeto `TcpClient` para interactuar con el dispositivo específico.
- **Validación de Datos:** Realiza el parsing de las tramas recibidas y convierte los datos de texto a valores numéricos (`double`) para su análisis.
- **Reglas de Negocio:** Incluye una lógica de monitoreo que dispara alertas visuales en consola ante picos de tensión (sobretensión > 240V).

### 2. El Cliente (Medidor Inteligente)

Simula un dispositivo físico conectado a la red eléctrica que envía telemetría de forma periódica.

- **Conexión y Flujo:** Utiliza `ConnectAsync` para establecer el enlace y `GetStream` para obtener el canal de comunicación.
- **Generación de Datos:** Simula variaciones reales de voltaje y amperaje, enviando la información codificada en bytes (UTF-8) a través del método `Write`.

## 📖 Conceptos Aplicados

A través de este desarrollo, se aplicaron conceptos fundamentales de redes y .NET:

- **Manejo de Buffers:** Gestión de arrays de bytes para la recepción y lectura eficiente de información.
- **Robustez:** Tratamiento de excepciones de red para detectar desconexiones abruptas y mantener la estabilidad del servidor.

## ⚙️ Cómo Ejecutar

1. Clonar el repositorio.
2. Ejecutar primero el proyecto **Server** para habilitar el puerto de escucha (Puerto 12345).
3. Ejecutar el proyecto **Client** para comenzar el envío de telemetría y observar la reconexión automática en caso de ser necesario.

---

*Proyecto desarrollado como parte de un proceso de investigación técnica en soluciones para el sector energético.*