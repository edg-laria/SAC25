

function ImprimirCtaBria(idNroCierre, idBancoCuenta, tablaJson, NombreCuenta, Fecha) {

    const doc = new jsPDF({
        orientation: "portrait",
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
    doc.text("Reporte de Cuenta Bancaria " + NombreCuenta, xText, 25);
    // Alinear el segundo texto a la derecha de la imagen
    doc.text("Cierre Nro " + idNroCierre.toString(), xText, 40);
    var textWidth = doc.getStringUnitWidth("Fecha Cierre  " + Fecha) * doc.internal.getFontSize() / doc.internal.scaleFactor;
    doc.text("Fecha Cierre  " + Fecha, doc.internal.pageSize.width - 20 - textWidth, 40);
    
    // Extraer los datos de la respuesta JSON
    const headers = tablaJson.headers;
    const data = tablaJson.data;

    // Calcular el margen izquierdo y derecho para centrar la tabla
    const tableWidth = 400; // Ancho deseado de la tabla (ajústalo según tus necesidades)
    const pageWidth = doc.internal.pageSize.width;
    const margin = (pageWidth - tableWidth) / 2;

    // Crear un array de arrays para los datos de la tabla
    //const tableData = data.map(row => Object.values(row));

    
    // Crear un array para almacenar los valores ocultos  
    const hiddenValues = [];

    // Formatear los datos antes de pasarlos a autoTable  
    const tableData = data.map(row => {
        // Omitir la columna que no deseas mostrar (por ejemplo, la columna en el índice 6)  
        const { 6: hiddenValue, ...rest } = row; // Suponiendo que la columna 6 es la que deseas ocultar  
        hiddenValues.push(hiddenValue); // Almacenar el valor oculto en el array  
        return Object.values(rest).map(value => {
            // Comprobar si el valor es numérico  
            if (typeof value === 'number') {
                return value === 0 ? '' : value.toFixed(2); // Formatear a 2 decimales o dejar en blanco si es 0  
            }
            return value; // Devolver el valor original si no es numérico  
        });
    });


    // Definir el ancho de cada columna
    const columnStyles = {
        0: { columnWidth: 180 }, // Ancho de la primera columna
        1: { columnWidth: 45 }, // Ancho de la segunda columna
        2: { columnWidth: 45 }, // Ancho de la segunda columna
        3: { columnWidth: 40, halign: 'right' }, // Formato para la cuarta columna
        4: { columnWidth: 40, halign: 'right' }, // Formato para la quinta columna
        5: { columnWidth: 50, halign: 'right' }, // Formato para la sexta columna
       // 6: { columnWidth: 0, overflow: 'ellipses'  } // Formato para la sexta columna
    };

    
    // Generar la tabla en el documento
    doc.autoTable({
        head: [headers], // Encabezados de la tabla
        body: tableData ,// Datos de la tabla
        tableWidth: 'wrap',
        horizontalPageBreak: true,

        startY: 60, // Ajustar la posición inicial de la tabla para dejar más espacio para el encabezado
        headStyles: { fillColor: [0, 128, 255], textColor: [255, 255, 255], fontSize: 8, cellPadding: 1 }, // Estilos del encabezado 
        styles: { cellPadding: 0.5, fontSize: 8, overflow: 'ellipses' },
        margin: { left: margin, right: margin }, // Centrar la tabla
        columnStyles: columnStyles, // Aplicar los estilos de columna

        didParseCell: function (data) {
            // Cambiar el color de fondo si el valor de la columna oculta es false  
            const hiddenValue = hiddenValues[data.row.index]; // Obtener el valor oculto usando el índice de la fila  
            if (hiddenValue === false) { // Verificar si es false  
                data.cell.styles.fillColor = [0, 255, 255]; // Cambiar a color deseado  
            }
        },

        willDrawPage: function (data) {
            // Header
            doc.setFontSize(20)
            doc.setTextColor(40)
            doc.text("Reporte de Cta Bancaria ", data.settings.margin.left + 15, 25, { align: "center" });
            doc.text("Cierre Nro " + idNroCierre.toString(), data.settings.margin.left + 15, 40, { align: "center" });
        },
        didDrawPage: function (data) {
            // Footer
            var str = 'Página ' + doc.internal.getNumberOfPages()
            // Total page number plugin only available in jspdf v1.0+
            if (typeof doc.putTotalPages === 'function') {
                str = str + ' de ' + totalPagesExp;
            }
            str = str + '                                             Fecha Impresion: ' + new Date().toLocaleDateString('es-LA', { year: 'numeric', month: 'short', day: '2-digit' });
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
    var NombreArchivo = "CierreCtaBcoN°"+idNroCierre.toString()+"  Banco "+idBancoCuenta.toString()+".pdf";
    doc.save(NombreArchivo);
}


function zfill(number, width) {
    var numberOutput = Math.abs(number); /* Valor absoluto del número */
    var length = number.toString().length; /* Largo del número */
    var zero = "0"; /* String de cero */

    if (width <= length) {
        if (number < 0) {
            return ("-" + numberOutput.toString());
        } else {
            return numberOutput.toString();
        }
    } else {
        if (number < 0) {
            return ("-" + (zero.repeat(width - length)) + numberOutput.toString());
        } else {
            return ((zero.repeat(width - length)) + numberOutput.toString());
        }
    }
}

// Uses the faker.js library to get random data.
function getData(rowCount) {
    rowCount = rowCount || 4;
    var sentence = faker.lorem.words(12);
    var data = [];
    for (var j = 1; j <= rowCount; j++) {
        data.push({
            id: j,
            first_name: faker.name.findName(),
            email: faker.internet.email(),
            country: faker.address.country(),
            city: faker.address.city(),
            expenses: faker.finance.amount(),
            text: shuffleSentence(sentence),
            text2: shuffleSentence(sentence)
        });
    }
    return data;
}

function shuffleSentence(words) {
    words = words || faker.lorem.words(8);
    var str = faker.helpers.shuffle(words).join(' ').trim();
    return str.charAt(0).toUpperCase() + str.slice(1);
}

// Use http://dopiaza.org/tools/datauri or similar service to convert an image into image data
//



var faker = window.faker

var examples = {}
window.examples = examples

// Basic - shows what a default table looks like
examples.basic = function () {
    var doc = new jsPDF()

    // From HTML
    doc.autoTable({ html: '.table' })

    // From Javascript
    var finalY = doc.lastAutoTable.finalY || 10
    doc.text('From javascript arrays', 14, finalY + 15)
    doc.autoTable({
        startY: finalY + 20,
        head: [['ID', 'Name', 'Email', 'Country', 'IP-address']],
        body: [
            ['1', 'Donna', 'dmoore0@furl.net', 'China', '211.56.242.221'],
            ['2', 'Janice', 'jhenry1@theatlantic.com', 'Ukraine', '38.36.7.199'],
            [
                '3',
                'Ruth',
                'rwells2@constantcontact.com',
                'Trinidad and Tobago',
                '19.162.133.184',
            ],
            ['4', 'Jason', 'jray3@psu.edu', 'Brazil', '10.68.11.42'],
            ['5', 'Jane', 'jstephens4@go.com', 'United States', '47.32.129.71'],
            ['6', 'Adam', 'anichols5@com.com', 'Canada', '18.186.38.37'],
        ],
    })

    finalY = doc.lastAutoTable.finalY
    doc.text('From HTML with CSS', 14, finalY + 15)
    doc.autoTable({
        startY: finalY + 20,
        html: '.table',
        useCss: true,
    })

    return doc
}

// Minimal - shows how compact tables can be drawn
examples.minimal = function () {
    var doc = new jsPDF()
    doc.autoTable({
        html: '.table',
        tableWidth: 'wrap',
        styles: { cellPadding: 0.5, fontSize: 8 },
    })
    return doc
}

// Long data - shows how the overflow features looks and can be used
examples.long = function () {
    var doc = new jsPDF('l')

    var head = headRows()
    head[0]['text'] = 'Text'
    var body = bodyRows(4)
    body.forEach(function (row) {
        row['text'] = faker.lorem.lorem.sentence(100)
    })

    doc.text("Overflow 'ellipsize' with one column with long content", 14, 20)
    doc.autoTable({
        head: head,
        body: body,
        startY: 25,
        // Default for all columns
        styles: { overflow: 'ellipsize', cellWidth: 'wrap' },
        // Override the default above for the text column
        columnStyles: { text: { cellWidth: 'auto' } },
    })
    doc.text(
        "Overflow 'linebreak' (default) with one column with long content",
        14,
        doc.lastAutoTable.finalY + 10
    )
    doc.autoTable({
        head: head,
        body: body,
        startY: doc.lastAutoTable.finalY + 15,
        rowPageBreak: 'auto',
        bodyStyles: { valign: 'top' },
    })

    return doc
}

// Content - shows how tables can be integrated with any other pdf content
examples.content = function () {
    var doc = new jsPDF()

    doc.setFontSize(18)
    doc.text('With content', 14, 22)
    doc.setFontSize(11)
    doc.setTextColor(100)

    // jsPDF 1.4+ uses getWidth, <1.4 uses .width
    var pageSize = doc.internal.pageSize
    var pageWidth = pageSize.width ? pageSize.width : pageSize.getWidth()
    var text = doc.splitTextToSize(faker.lorem.sentence(45), pageWidth - 35, {})
    doc.text(text, 14, 30)

    doc.autoTable({
        head: headRows(),
        body: bodyRows(40),
        startY: 50,
        showHead: 'firstPage',
    })

    doc.text(text, 14, doc.lastAutoTable.finalY + 10)

    return doc
}

// Multiple - shows how multiple tables can be drawn both horizontally and vertically
examples.multiple = function () {
    var doc = new jsPDF()
    doc.text('Multiple tables', 14, 20)

    doc.autoTable({ startY: 30, head: headRows(), body: bodyRows(25) })

    var pageNumber = doc.internal.getNumberOfPages()

    doc.autoTable({
        columns: [
            { dataKey: 'id', header: 'ID' },
            { dataKey: 'name', header: 'Name' },
            { dataKey: 'expenses', header: 'Sum' },
        ],
        body: bodyRows(15),
        startY: 240,
        showHead: 'firstPage',
        styles: { overflow: 'hidden' },
        margin: { right: 107 },
    })

    doc.setPage(pageNumber)

    doc.autoTable({
        columns: [
            { dataKey: 'id', header: 'ID' },
            { dataKey: 'name', header: 'Name' },
            { dataKey: 'expenses', header: 'Sum' },
        ],
        body: bodyRows(15),
        startY: 240,
        showHead: 'firstPage',
        styles: { overflow: 'hidden' },
        margin: { left: 107 },
    })

    for (var j = 0; j < 3; j++) {
        doc.autoTable({
            head: headRows(),
            body: bodyRows(),
            startY: doc.lastAutoTable.finalY + 10,
            pageBreak: 'avoid',
        })
    }

    return doc
}

// Header and footers - shows how header and footers can be drawn
examples['header-footer'] = function () {
    var doc = new jsPDF()
    var totalPagesExp = '{total_pages_count_string}'

    doc.autoTable({
        head: headRows(),
        body: bodyRows(40),
        willDrawPage: function (data) {
            // Header
            doc.setFontSize(20)
            doc.setTextColor(40)
            if (base64Img) {
                doc.addImage(base64Img, 'JPEG', data.settings.margin.left, 15, 10, 10)
            }
            doc.text('Report', data.settings.margin.left + 15, 22)
        },
        didDrawPage: function (data) {
            // Footer
            var str = 'Page ' + doc.internal.getNumberOfPages()
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
    })

    // Total page number plugin only available in jspdf v1.0+
    if (typeof doc.putTotalPages === 'function') {
        doc.putTotalPages(totalPagesExp)
    }

    return doc
}

// Minimal - shows how compact tables can be drawn
examples.defaults = function () {
    // Global defaults
    // (would apply to all documents if more than one were created)
    jsPDF.autoTableSetDefaults({
        headStyles: { fillColor: 0 },
    })

    var doc = new jsPDF()

    doc.text('Global options (black header)', 15, 20)
    doc.autoTable({ head: headRows(), body: bodyRows(5), startY: 25 })

    // Document defaults
    jsPDF.autoTableSetDefaults(
        {
            headStyles: { fillColor: [155, 89, 182] }, // Purple
            didDrawPage: function (data) {
                var finalY = doc.lastAutoTable.finalY + 15
                var leftMargin = data.settings.margin.left
                doc.text('Default options (purple header)', leftMargin, finalY)
            },
        },
        doc
    )

    var startY = doc.lastAutoTable.finalY + 20
    doc.autoTable({ head: headRows(), body: bodyRows(5), startY: startY })

    // Reset defaults
    doc.autoTableSetDefaults(null)
    jsPDF.autoTableSetDefaults(null)

    var finalY = doc.lastAutoTable.finalY
    doc.text('After reset (blue header)', 15, finalY + 15)
    doc.autoTable({ head: headRows(), body: bodyRows(5), startY: finalY + 20 })

    return doc
}

// Column styles - shows how tables can be drawn with specific column styles
examples.colstyles = function () {
    var doc = new jsPDF()
    doc.autoTable({
        head: headRows(),
        body: bodyRows(),
        showHead: false,
        // Note that the "id" key below is the same as the column's dataKey used for
        // the head and body rows. If your data is entered in array form instead you have to
        // use the integer index instead i.e. `columnStyles: {5: {fillColor: [41, 128, 185], ...}}`
        columnStyles: {
            id: { fillColor: [41, 128, 185], textColor: 255, fontStyle: 'bold' },
        },
    })

    return doc
}

// Col spans and row spans
examples.spans = function () {
    var doc = new jsPDF('p', 'pt')
    doc.text('Rowspan and colspan', 40, 50)

    var raw = bodyRows(40)
    var body = []

    for (var i = 0; i < raw.length; i++) {
        var row = []
        for (var key in raw[i]) {
            row.push(raw[i][key])
        }
        if (i % 5 === 0) {
            row.unshift({
                rowSpan: 5,
                content: i / 5 + 1,
                styles: { valign: 'middle', halign: 'center' },
            })
        }
        body.push(row)
    }

    doc.autoTable({
        startY: 60,
        head: [
            [
                {
                    content: 'People',
                    colSpan: 5,
                    styles: { halign: 'center', fillColor: [22, 160, 133] },
                },
            ],
        ],
        body: body,
        theme: 'grid',
    })
    return doc
}

// Themes - shows how the different themes looks
examples.themes = function () {
    var doc = new jsPDF()

    doc.text('Theme "striped"', 14, 16)
    doc.autoTable({ head: headRows(), body: bodyRows(5), startY: 20 })

    doc.text('Theme "grid"', 14, doc.lastAutoTable.finalY + 10)
    doc.autoTable({
        head: headRows(),
        body: bodyRows(5),
        startY: doc.lastAutoTable.finalY + 14,
        theme: 'grid',
    })

    doc.text('Theme "plain"', 14, doc.lastAutoTable.finalY + 10)
    doc.autoTable({
        head: headRows(),
        body: bodyRows(5),
        startY: doc.lastAutoTable.finalY + 14,
        theme: 'plain',
    })

    return doc
}

// Nested tables
examples.nested = function () {
    var doc = new jsPDF()
    doc.text('Nested tables', 14, 16)

    var nestedTableHeight = 100
    var nestedTableCell = {
        content: '',
        // Dynamic height of nested tables are not supported right now
        // so we need to define height of the parent cell
        styles: { minCellHeight: 100 },
    }
    doc.autoTable({
        theme: 'grid',
        head: [['2019', '2020']],
        body: [[nestedTableCell]],
        foot: [['2019', '2020']],
        startY: 20,
        didDrawCell: function (data) {
            if (data.row.index === 0 && data.row.section === 'body') {
                doc.autoTable({
                    startY: data.cell.y + 2,
                    margin: { left: data.cell.x + 2 },
                    tableWidth: data.cell.width - 4,
                    styles: {
                        maxCellHeight: 4,
                    },
                    columns: [
                        { dataKey: 'id', header: 'ID' },
                        { dataKey: 'name', header: 'Name' },
                        { dataKey: 'expenses', header: 'Sum' },
                    ],
                    body: bodyRows(),
                })
            }
        },
    })

    return doc
}

// Custom style - shows how custom styles can be applied
examples.custom = function () {
    var doc = new jsPDF()
    doc.autoTable({
        head: headRows(),
        body: bodyRows(),
        foot: headRows(),
        margin: { top: 37 },
        tableLineColor: [231, 76, 60],
        tableLineWidth: 1,
        styles: {
            lineColor: [44, 62, 80],
            lineWidth: 1,
        },
        headStyles: {
            fillColor: [241, 196, 15],
            fontSize: 15,
        },
        footStyles: {
            fillColor: [241, 196, 15],
            fontSize: 15,
        },
        bodyStyles: {
            fillColor: [52, 73, 94],
            textColor: 240,
        },
        alternateRowStyles: {
            fillColor: [74, 96, 117],
        },
        // Note that the "email" key below is the same as the column's dataKey used for
        // the head and body rows. If your data is entered in array form instead you have to
        // use the integer index instead i.e. `columnStyles: {5: {fillColor: [41, 128, 185], ...}}`
        columnStyles: {
            email: {
                fontStyle: 'bold',
            },
            city: {
                // The font file mitubachi-normal.js is included on the page and was created from mitubachi.ttf
                // with https://rawgit.com/MrRio/jsPDF/master/fontconverter/fontconverter.html
                // refer to https://github.com/MrRio/jsPDF#use-of-utf-8--ttf
                font: 'mitubachi',
            },
            id: {
                halign: 'right',
            },
        },
        allSectionHooks: true,
        // Use for customizing texts or styles of specific cells after they have been formatted by this plugin.
        // This hook is called just before the column width and other features are computed.
        didParseCell: function (data) {
            if (data.row.index === 5) {
                data.cell.styles.fillColor = [40, 170, 100]
            }

            if (
                (data.row.section === 'head' || data.row.section === 'foot') &&
                data.column.dataKey === 'expenses'
            ) {
                data.cell.text = '' // Use an icon in didDrawCell instead
            }

            if (data.column.dataKey === 'city') {
                data.cell.styles.font = 'mitubachi'
                if (data.row.section === 'head') {
                    data.cell.text = 'シティ'
                }
                if (data.row.index === 0 && data.row.section === 'body') {
                    data.cell.text = 'とうきょう'
                }
            }
        },
        // Use for changing styles with jspdf functions or customize the positioning of cells or cell text
        // just before they are drawn to the page.
        willDrawCell: function (data) {
            if (data.row.section === 'body' && data.column.dataKey === 'expenses') {
                if (data.cell.raw > 750) {
                    doc.setTextColor(231, 76, 60) // Red
                }
            }
        },
        // Use for adding content to the cells after they are drawn. This could be images or links.
        // You can also use this to draw other custom jspdf content to cells with doc.text or doc.rect
        // for example.
        didDrawCell: function (data) {
            if (
                (data.row.section === 'head' || data.row.section === 'foot') &&
                data.column.dataKey === 'expenses' &&
                coinBase64Img
            ) {
                doc.addImage(
                    coinBase64Img,
                    'PNG',
                    data.cell.x + 5,
                    data.cell.y + 2,
                    5,
                    5
                )
            }
        },
        // Use this to add content to each page that has the autoTable on it. This can be page headers,
        // page footers and page numbers for example.
        didDrawPage: function (data) {
            doc.setFontSize(18)
            doc.text('Custom styling with hooks', data.settings.margin.left, 22)
            doc.setFontSize(12)
            doc.text(
                'Conditional styling of cells, rows and columns, cell and table borders, custom font, image in cell',
                data.settings.margin.left,
                30
            )
        },
    })
    return doc
}

// Custom style - shows how custom styles can be applied
examples.borders = function () {
    var doc = new jsPDF()
    doc.autoTable({
        head: headRows(),
        body: bodyRows(3),
        foot: [
            [
                {
                    content: 'ID',
                    dataKey: 'id',
                    styles: {
                        fillColor: [255, 0, 0],
                        lineWidth: 1,
                        lineColor: 'black',
                    },
                },
                {
                    content: 'Name',
                    dataKey: 'name',
                    styles: {
                        fillColor: [0, 255, 0],
                    },
                },
                {
                    content: 'Email',
                    dataKey: 'email',
                    styles: {
                        fillColor: [0, 0, 255],
                        lineWidth: 2,
                        lineColor: 'yellow',
                    },
                },
                {
                    content: 'City',
                    dataKey: 'city',
                    styles: {
                        fillColor: [0, 255, 0],
                        lineWidth: 0.5,
                    },
                },
                {
                    content: 'Sum',
                    dataKey: 'sum',
                    styles: {
                        textColor: 'white',
                        fillColor: [255, 0, 0],
                        lineColor: 'black',
                        lineWidth: {
                            right: 2,
                            bottom: 3,
                            top: 1,
                            left: 6,
                        },
                    },
                },
            ],
        ],
        margin: { top: 40 },
        theme: 'plain',
        headStyles: {
            fillColor: '#f1c40f',
            fontSize: 15,
            lineWidth: {
                top: 1
            }
        },
        footStyles: {
            fillColor: [241, 196, 15],
            fontSize: 15,
        },
        // Note that the "email" key below is the same as the column's dataKey used for
        // the head and body rows. If your data is entered in array form instead you have to
        // use the integer index instead i.e. `columnStyles: {5: {fillColor: [41, 128, 185], ...}}`
        columnStyles: {
            email: {
                fontStyle: 'bold',
            },
            city: {
                // The font file mitubachi-normal.js is included on the page and was created from mitubachi.ttf
                // with https://rawgit.com/MrRio/jsPDF/master/fontconverter/fontconverter.html
                // refer to https://github.com/MrRio/jsPDF#use-of-utf-8--ttf
                font: 'mitubachi',
            },
            id: {
                halign: 'right',
            },
        },
        allSectionHooks: true,
        // Use for customizing texts or styles of specific cells after they have been formatted by this plugin.
        // This hook is called just before the column width and other features are computed.
        didParseCell: function (data) {
            if (
                data.row.section === 'head' &&
                ['name', 'city'].includes(data.column.dataKey)
            ) {
                data.cell.styles.lineColor = 'black'
                data.cell.styles.lineWidth = {
                    bottom: 1,
                }
            }
            if (data.row.section === "body" && data.row.index === 1 && data.cell === data.row.cells[1]) {
                data.cell.styles.fillColor = '#f1c40f' // cell background color
                data.cell.styles.lineColor = 'red' // cell border color
                data.cell.styles.lineWidth = {
                    bottom: 1, // only bottom border will be painted
                }
            }
        },
        // Use this to add content to each page that has the autoTable on it. This can be page headers,
        // page footers and page numbers for example.
        didDrawPage: function (data) {
            doc.setFontSize(18)
            doc.text('Custom borders showcase', data.settings.margin.left, 22)
            doc.setFontSize(12)
            doc.text(
                'Borders are drawn just at the edge of the cell, which means that half of the border is in the cell and the other half is outside.',
                data.settings.margin.left,
                30,
                { maxWidth: 180 }
            )
        },
    })
    return doc
}

// Split columns - shows how the overflowed columns split into pages
examples.horizontalPageBreak = function () {
    var doc = new jsPDF('l')

    var head = headRows()
    head[0].region = 'Region'
    head[0].country = 'Country'
    head[0].zipcode = 'Zipcode'
    head[0].phone = 'Phone'
    // head[0].timeZone = 'Timezone';
    head[0]['text'] = 'Text'
    var body = bodyRows(4)
    body.forEach(function (row) {
        row['text'] = faker.lorem.sentence(100)
        row['zipcode'] = faker.address.zipCode()
        row['country'] = faker.address.country()
        row['region'] = faker.address.state()
        row['phone'] = faker.phone.phoneNumber()
        // row['timeZone'] = faker.address.timeZone();
    })

    doc.text('Split columns across pages if not fit in a single page', 14, 20)
    doc.autoTable({
        head: head,
        body: body,
        startY: 25,
        // split overflowing columns into pages
        horizontalPageBreak: true,
        // repeat this column in split pages
        // horizontalPageBreakRepeat: 'id',
    })
    return doc
}

// Split columns - shows how the overflowed columns split into pages with a given column repeated
examples.horizontalPageBreakRepeat = function () {
    var doc = new jsPDF('l')

    var head = headRows()
    head[0].region = 'Region'
    head[0].country = 'Country'
    head[0].zipcode = 'Zipcode'
    head[0].phone = 'Phone'
    // head[0].timeZone = 'Timezone';
    head[0]['text'] = 'Text'
    var body = bodyRows(4)
    body.forEach(function (row) {
        row['text'] = faker.lorem.sentence(15)
        row['zipcode'] = faker.address.zipCode()
        row['country'] = faker.address.country()
        row['region'] = faker.address.state()
        row['phone'] = faker.phone.phoneNumber()
        // row['timeZone'] = faker.address.timeZone();
    })

    doc.text(
        'Split columns across pages if not fit in a single page with a column repeated.',
        14,
        20
    )
    doc.autoTable({
        head: head,
        body: body,
        startY: 25,
        // split overflowing columns into pages
        horizontalPageBreak: true,
        // repeat this column in split pages
        horizontalPageBreakRepeat: 'id',
    })
    return doc
}

// Split columns - shows how to alter the behaviour of columns that split into pages
examples.horizontalPageBreakBehaviour = function () {
    var doc = new jsPDF('l')

    var head = headRows()
    head[0].region = 'Region'
    head[0].country = 'Country'
    head[0].zipcode = 'Zipcode'
    head[0].phone = 'Phone'
    head[0].datetime = 'DateTime'
    head[0].text = 'Text'
    var body = bodyRows(50)
    body.forEach(function (row) {
        row['text'] = faker.lorem.sentence(10)
        row['zipcode'] = faker.address.zipCode()
        row['country'] = faker.address.country()
        row['region'] = faker.address.state()
        row['phone'] = faker.phone.phoneNumber()
        row['datetime'] = faker.date.recent()
    })

    doc.text('Split columns across pages if not fit in a single page, showing all the columns first', 14, 20)
    doc.autoTable({
        head: head,
        body: body,
        startY: 25,
        // split overflowing columns into pages
        horizontalPageBreak: true,
        horizontalPageBreakBehaviour: 'immediately',
        // repeat this column in split pages
        //horizontalPageBreakRepeat: 'id',
    })
    return doc
}

/*
 |--------------------------------------------------------------------------
 | Below is some helper functions for the examples
 |--------------------------------------------------------------------------
 */

function headRows() {
    return [
        { id: 'ID', name: 'Name', email: 'Email', city: 'City', expenses: 'Sum' },
    ]
}

function footRows() {
    return [
        { id: 'ID', name: 'Name', email: 'Email', city: 'City', expenses: 'Sum' },
    ]
}

function columns() {
    return [
        { header: 'ID', dataKey: 'id' },
        { header: 'Name', dataKey: 'name' },
        { header: 'Email', dataKey: 'email' },
        { header: 'City', dataKey: 'city' },
        { header: 'Exp', dataKey: 'expenses' },
    ]
}

function data(rowCount) {
    rowCount = rowCount || 10
    var body = []
    for (var j = 1; j <= rowCount; j++) {
        body.push({
            id: j,
            name: faker.name.findName(),
            email: faker.internet.email(),
            city: faker.address.city(),
            expenses: faker.finance.amount(),
        })
    }
    return body
}

function bodyRows(rowCount) {
    rowCount = rowCount || 10
    var body = []
    for (var j = 1; j <= rowCount; j++) {
        body.push({
            id: j,
            name: faker.name.findName(),
            email: faker.internet.email(),
            city: faker.address.city(),
            expenses: faker.finance.amount(),
        })
    }
    return body
}