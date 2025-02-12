


function ImprimirCtaCteResumen(Data, header, selectedDate) {

    const doc = new jsPDF({
        orientation: "lancast",
        unit: "px",
        format: "A4"
    });

    doc.addFont('ArialUnicodeMS-normal.ttf', 'ArialUnicodeMS', 'normal');
    doc.setFont('ArialUnicodeMS');

    var pageNumber = doc.internal.getNumberOfPages()
    var totalPagesExp = '{total_pages_count_string}'

    var xImage = 25; // Posición X de la imagen
    var xText = xImage + 60 ; // Posición X del texto a la derecha de la imagen
  
    doc.setPage(pageNumber)
    doc.setFontSize(15)
    base64Img = "data:image/jpeg;base64,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"
    doc.addImage(base64Img, 'JPG', xImage, 20, 50, 25)
    // Alinear el primer texto a la derecha de la imagen
    doc.text("Reporte Cuenta Corriente Clientes Resumen ", xText, 25);
    doc.text("Saldos al " + selectedDate, xText, 45);

      // Extraer los datos de la respuesta JSON
    const headers =header;
    const data = Data;

    // Calcular el margen izquierdo y derecho para centrar la tabla
    const tableWidth = 600; // Ancho deseado de la tabla (ajústalo según tus necesidades)
    const pageWidth = doc.internal.pageSize.width;
    const margin = (pageWidth - tableWidth) / 2;

    // Crear un array de arrays para los datos de la tabla
   // const tableData = data.map(row => Object.values(row));

    // Definir el ancho de cada columna
    const columnStyles = {
        0: { columnWidth: 40 },
        1: { columnWidth: 180 },
        2: { columnWidth: 40 },
        3: { columnWidth: 80 },
        4: { columnWidth: 80 },
        5: { columnWidth: 80 },
        6: { columnWidth: 40}
        };

       // Crear un array de arrays para los datos de la tabla

    const tableData = data.map((rowData) => {
        return Object.values(rowData).map((value) => {
            if (typeof value === 'string') {
                // Verificar si está en el formato milisegundos  
                const millisecondsMatch = String(value).match(/\/Date\((\d+)\)\//);
                if (millisecondsMatch) {
                    const milliseconds = parseInt(millisecondsMatch[1], 10);
                    const date = new Date(milliseconds);
                    return date.toLocaleDateString('es-ES'); // Formato dd/mm/aaaa  
                } else {
                    // Eliminar comillas de los strings  
                    return value.replace(/"/g, '');
                }
            } else if (typeof value === 'number' || !isNaN(Date.parse(value))) {
                // Si value es un número o puede ser convertido a fecha  
                // Verificar si está en el formato milisegundos  
                const millisecondsMatch = String(value).match(/\/Date\((\d+)\)\//);
                if (millisecondsMatch) {
                    const milliseconds = parseInt(millisecondsMatch[1], 10);
                    const date = new Date(milliseconds);
                    return date.toLocaleDateString('es-ES'); // Formato dd/mm/aaaa  
                } else {
                    const cleanedValue = parseFloat(String(value).replace(/[^0-9.-]/g, '')).toFixed(2);
                    return isNaN(cleanedValue) ? 0 : cleanedValue;
                }
            } else {
                return value; // Retornar el valor tal cual si no es string, número, o milisegundos  
            }
        });
    });

    // Generar la tabla con jspdf-autotable
    doc.autoTable({
        head: [headers], // Encabezados de la tabla
        body: tableData, // Datos de la tabla
        tableWidth: 'wrap',
        horizontalPageBreak: true,
        startY: 60, // Ajustar la posición inicial de la tabla para dejar más espacio para el encabezado
        headStyles: { fillColor: [0, 128, 255], textColor: [255, 255, 255], fontSize: 8, cellPadding: 1 }, // Estilos del encabezado 
        styles: { cellPadding: 0.5, fontSize: 8 },
        margin: { left: margin, right: margin }, // Centrar la tabla
        columnStyles: columnStyles, // Aplicar los estilos de columna
       
        // Callback para modificar las celdas  
        didParseCell: function (data) {
            // Verifica si la celda pertenece a una fila que contiene 'Total'  
            if (data.row.index >= 0 && (tableData[data.row.index][0] === 'Total' || tableData[data.row.index][0] === 'Local' || tableData[data.row.index][0] === 'Exterior' ) ) {
                
                // Aplica estilos a toda la fila
                data.cell.styles.fontStyle = 'bold'; // Estilo de negrita  
                data.cell.styles.fontSize = 10; // tamaño de letra
                data.cell.styles.textColor = [255, 255, 255];
                data.cell.styles.fillColor = [0, 128, 255]; // Color de fondo
                data.cell.styles.overflow = 'linebreak'; // Asegúrate de que no sea 'hidden'  
                data.cell.styles.whiteSpace = 'normal'; // Esto permite el ajuste de línea si es necesario  
               
            }
        },
        willDrawPage: function (data) {
            // Header
            doc.setFontSize(20)
            doc.setTextColor(40)
            doc.text("Reporte de Ctas. Ctes. Resumen ", data.settings.margin.left + 15, 25, { align: "center" });
            
        },
        didDrawPage: function (data) {
            // Footer
            var str = 'Página ' + doc.internal.getNumberOfPages()
            // Total page number plugin only available in jspdf v1.0+
            if (typeof doc.putTotalPages === 'function') {
                str = str + ' of ' + totalPagesExp
            }
            doc.setFontSize(10)

            // jsPDF 1.4+ uses getHeight, <1.4 uses .height
            var pageSize = doc.internal.pageSize
            var pageHeight = pageSize.height ? pageSize.height : pageSize.getHeight()
            doc.text(str, data.settings.margin.left, pageHeight - 10)
        },
        margin: { top: 30 },
    });

    // Total page number plugin only available in jspdf v1.0+
    if (typeof doc.putTotalPages === 'function') {
        doc.putTotalPages(totalPagesExp)
    }

    var Reporte = 'Reporte_CtaCte_Resumen.pdf'
    doc.save(Reporte);
}

