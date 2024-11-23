function printRecipeCard() {
    const customStyles = `
        /* Resetting some default styles */
        body {
            font-family: 'Arial', sans-serif;
            font-size: 12pt;
            margin: 0 20mm;
            line-height: 1.5;
        }

        /* Recipe card layout */
        .card {
            width: 100%;
            margin: 10px 0;
            border: 1px solid #ddd;
            padding: 20px;
            page-break-inside: avoid;
        }

        .card-header {
            border-bottom: 1px solid #ddd;
            margin-bottom: 10px;
        }

        .card-body {
            padding: 10px 0;
        }

        /* Recipe title */
        .card-title {
            font-size: 20pt;
            font-weight: bold;
            text-align: center;
            margin-bottom: 10px;
        }

        .recipe-author-card {
            text-align: center;
        }

        .recipe-author-card a {
            text-decoration: none;
        }

        .recipe-serving-size {
            text-align: center;
        }

        /* Info section (Prep, Cook time, etc.) */
        .card-info {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            margin-bottom: 20px;
        }

        .card-info p {
            font-size: 14pt;
            margin: 5px 15px;
            text-align: center;
        }

        /* Ingredients and instructions columns */
        .recipe-ingredients-title,
        .recipe-instructions-title {
            font-size: 16pt;
            font-weight: bold;
            margin-top: 20px;
        }

        .ingredients-list,
        .instructions-list {
            font-size: 14pt;
            margin-top: 5px;
        }

        .instructions-list {
            margin-left: -10px;
            list-style-type: none;
            padding-left: 5px;
        }

        .ingredients-list li,
        .instructions-list li {
            margin-bottom: 10px;
        }

        .recipe-ingredients,
        .recipe-instructions {
            font-size: 14pt;
        }

        /* Remove non-printable elements */
        .card-btns,
        .no-print {
            display: none;
        }

        /* Page margins and print-specific settings */
        @media print {
            @page {
                margin: 20mm;
            }

            .recipe-title {
                font-size: 24pt;
            }

            .recipe-ingredients,
            .recipe-instructions {
                font-size: 14pt;
            }
        }
    `;

    console.log("Printing Recipe Card");
    printJS({
        printable: 'full-recipe-card',
        type: 'html',
        style: customStyles
    });
}
