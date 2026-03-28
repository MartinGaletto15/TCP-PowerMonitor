# TCP PowerMonitor: Simulador de Telemetría Industrial en .NET

Este proyecto es una **Prueba de Concepto (PoC)** diseñada para simular la captura y procesamiento de datos de medidores eléctricos en tiempo real mediante el protocolo **TCP/IP**. [cite_start]Está orientado a demostrar la capacidad de manejar comunicaciones de bajo nivel, asincronismo y resiliencia en sistemas críticos de energía.

## 🚀 Características Técnicas
* [cite_start]**Comunicación Persistente:** Implementación de un flujo de datos continuo sobre el protocolo TCP utilizando `NetworkStream`[cite: 5].
* [cite_start]**Arquitectura Resiliente:** El cliente posee una lógica de **reconexión automática** que se activa ante fallos en el servidor o caídas de red, asegurando la continuidad del servicio[cite: 15].
* [cite_start]**Procesamiento Asincrónico:** Uso de métodos asíncronos como `AcceptTcpClientAsync` y `ReadAsync` para evitar el bloqueo del hilo principal y mejorar el rendimiento[cite: 10, 13].
* [cite_start]**Parsing de Protocolo de Aplicación:** Implementación de una estructura de mensajes personalizada (basada en delimitadores `|` y `:`) para traducir bytes en métricas eléctricas procesables[cite: 14].

## 🛠️ Estructura del Sistema

### [cite_start]1. El Servidor (Central de Monitoreo) [cite: 8]
Actúa como el concentrador de datos. [cite_start]Utiliza `TcpListener` para abrir un puerto de escucha y aceptar conexiones entrantes[cite: 8, 9].
* [cite_start]**Gestión de Conexiones**: Al recibir una conexión, se genera un objeto `TcpClient` para interactuar con el dispositivo específico[cite: 10, 11].
* [cite_start]**Validación de Datos**: Realiza el parsing de las tramas recibidas y convierte los datos de texto a valores numéricos (`double`) para su análisis[cite: 14].
* [cite_start]**Reglas de Negocio**: Incluye una lógica de monitoreo que dispara alertas visuales en consola ante picos de tensión (sobretensión > 240V)[cite: 14].

### [cite_start]2. El Cliente (Medidor Inteligente) [cite: 1]
[cite_start]Simula un dispositivo físico conectado a la red eléctrica que envía telemetría de forma periódica[cite: 15].
* [cite_start]**Conexión y Flujo**: Utiliza `ConnectAsync` para establecer el enlace y `GetStream` para obtener el canal de comunicación[cite: 3, 4].
* [cite_start]**Generación de Datos**: Simula variaciones reales de voltaje y amperaje, enviando la información codificada en bytes (UTF-8) a través del método `Write`[cite: 6].

## 📖 Conceptos Aplicados
A través de este desarrollo, se aplicaron conceptos fundamentales de redes y .NET:
* [cite_start]**Ciclo de Vida del Socket**: Manejo desde la instanciación y `Start()` hasta la liberación de recursos mediante el método `Close()` y sentencias `using`[cite: 7, 9, 12].
* [cite_start]**Manejo de Buffers**: Gestión de arrays de bytes para la recepción y lectura eficiente de información[cite: 6].
* [cite_start]**Robustez**: Tratamiento de excepciones de red para detectar desconexiones abruptas y mantener la estabilidad del servidor[cite: 13, 15].

## ⚙️ Cómo Ejecutar
1. Clonar el repositorio.
2. Ejecutar primero el proyecto **Server** para habilitar el puerto de escucha (Puerto 12345).
3. Ejecutar el proyecto **Client** para comenzar el envío de telemetría y observar la reconexión automática en caso de ser necesario.

---

### 📝 Roadmap / Futuras Mejoras
* [ ] **Serialización Binaria**: Migrar de strings a un protocolo binario puro para reducir el consumo de ancho de banda.
* [ ] **Seguridad**: Integrar TLS/SSL para cifrar la comunicación entre medidores y la central.
* [ ] **Escalabilidad**: Implementar manejo de múltiples clientes simultáneos mediante hilos dedicados o tareas independientes.

---
*Proyecto desarrollado como parte de un proceso de investigación técnica en soluciones para el sector energético.*