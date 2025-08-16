# DeepL Translator - Windows Forms .NET 9

Una aplicación moderna de traducción de texto que utiliza la API de DeepL para proporcionar traducciones de alta calidad.

## Características

- 🌐 **Detección automática de idioma**: Detecta automáticamente el idioma del texto de entrada
- 🔄 **Traducción en tiempo real**: Traduce texto usando la potente API de DeepL
- 🔊 **Text-to-Speech**: Escucha la pronunciación de las traducciones
- 📋 **Copiar al portapapeles**: Copia fácilmente las traducciones
- 🎨 **Interfaz moderna**: Diseño limpio y moderno con Windows Forms
- 🚀 **Soporte para múltiples idiomas**: Más de 15 idiomas soportados

## Requisitos

- .NET 9.0 o superior
- Windows 10/11
- Conexión a Internet para las traducciones
- API Key de DeepL (ya incluida en el código)

## Instalación y Uso

1. **Clonar o descargar el proyecto**
2. **Abrir en Visual Studio 2022** (con soporte para .NET 9)
3. **Restaurar paquetes NuGet**:
   - Newtonsoft.Json
   - System.Speech
4. **Compilar y ejecutar** (F5)

## Cómo usar la aplicación

1. **Escribir texto**: Introduce el texto que deseas traducir en el área superior
2. **Seleccionar idioma**: Elige el idioma de destino del menú desplegable
3. **Traducir**: Haz clic en "Translate" o presiona Ctrl+Enter
4. **Escuchar**: Usa el botón "Listen" para escuchar la pronunciación
5. **Copiar**: Usa el botón "Copy" para copiar la traducción al portapapeles

## Idiomas Soportados

- 🇺🇸 English (US)
- 🇬🇧 English (UK)
- 🇪🇸 Spanish
- 🇫🇷 French
- 🇩🇪 German
- 🇮🇹 Italian
- 🇵🇹 Portuguese
- 🇧🇷 Portuguese (Brazil)
- 🇷🇺 Russian
- 🇯🇵 Japanese
- 🇨🇳 Chinese
- 🇰🇷 Korean
- 🇳🇱 Dutch
- 🇵🇱 Polish
- 🇸🇪 Swedish
- 🇩🇰 Danish
- 🇳🇴 Norwegian
- 🇫🇮 Finnish

## Atajos de Teclado

- **Ctrl+Enter**: Traducir texto
- **Ctrl+C**: Copiar traducción (cuando el área de salida está seleccionada)

## Arquitectura

La aplicación está estructurada de la siguiente manera:

- **Models**: Modelos de datos para las respuestas de la API
- **Services**: Servicios para DeepL API y Text-to-Speech
- **MainForm**: Interfaz principal de usuario

## API de DeepL

Esta aplicación utiliza la API gratuita de DeepL. La clave API está incluida en el código, pero puedes reemplazarla con tu propia clave si es necesario.

## Solución de Problemas

1. **Error de conexión**: Verifica tu conexión a Internet
2. **Error de API**: Asegúrate de que la clave API sea válida
3. **Problemas de audio**: Verifica que tu sistema tenga voces TTS instaladas
4. **Problemas de DPI**: La aplicación incluye soporte para DPI alto

## Personalización

Puedes personalizar la aplicación modificando:

- **Colores**: Cambia los colores en el método `CreateModernButton`
- **Fuentes**: Modifica las fuentes en `InitializeComponent`
- **Idiomas**: Añade más idiomas en `LanguageService`
- **API Key**: Reemplaza la clave en `MainForm` constructor

## Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.
