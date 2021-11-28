using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXmlDocs.Extensions;

namespace OpenXmlDocs
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var title = args.InitTitle();
            var path = App.InitPath(title);

            var res = await GenerateDocument(path);
            if (res) Console.WriteLine("Document successfully created!");

            res = await ModifyDocument(path);
            if (res) Console.WriteLine("Document successfully modified!");
        }

        static Task<bool> GenerateDocument(string path) => Task.Run(() =>
        {

            using var file = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);
            var doc = file.InitDocument();

            var markings = Builder.GenerateMarkings(
                doc.Main,
                new Text("HEADER//CENTERED"),
                new Text("FOOTER//CENTERED")
            );

            doc.Body.InitMarkings(markings);

            var card = Builder.GenerateImageCard(
                doc.Main,
                App.GetAssetFile("atom.png"),
                "Generated Documents",
                "Some sub-details",
                "some more sub-details"
            );

            var para = Builder.GenerateParagraph(new Text("This was generated automatically!"));

            doc.Append(card, para);

            return true;
        });

        static Task<bool> ModifyDocument(string path) => Task.Run(() =>
        {
            using var file = WordprocessingDocument.Open(path, true);
            var doc = file.InitDocument(false);

            var layout = Layout.Base(b => b.Append(
                Layout.Row(row => row.Append(
                    Layout.Content(
                        Builder.GenerateParagraph(new Text("Justified Left"))
                    ),
                    Layout.Content(
                        Builder.GenerateParagraph(
                            new Text("Justified Center"),
                            () => new ParagraphProperties(new Justification() { Val = JustificationValues.Center })
                        )
                    ),
                    Layout.Content(
                        Builder.GenerateParagraph(
                            new Text("Justified Right"),
                            () => new ParagraphProperties(new Justification() { Val = JustificationValues.End })
                        )
                    )
                )),
                Layout.Row(row => row.Append(
                    Layout.Content(
                        Builder.GenerateParagraph(
                            new Text("A long block of text that will span all three columns of the table!"),
                            () => new ParagraphProperties(new Justification() { Val = JustificationValues.Center })
                        ),
                        () => new TableCellProperties(
                            new GridSpan() { Val = 3 }
                        )
                    )
                ))
            ));

            var demo = Layout.Base(b => b.Append(
                Layout.Row(row => row.Append(
                    Layout.Content(
                        () => new TableCellProperties(
                            new GridSpan() { Val = 3 }
                        ),
                        Builder.GenerateParagraph(
                            new Text("Man, that guy is the Red Grin Grumbold of pretending he knows what's going on. Oh you agree huh? You like that Red Grin Grumbold reference? Well guess what, I made him up. You really are your father's children. Think for yourselves, don't be sheep. Awww, it's you guys! Come on, flip the pickle, Morty. You're not gonna regret it. The payoff is huge. I was just killing some snaked up here like everyone else, I guess, and finishing the Christmas lights.")
                        ),
                        Builder.GenerateParagraph(
                            new Text("Looks like some sort of legally safe knockoff of an '80s horror character with miniature swords for fingers instead of knives! Allahu blehhhh Akbar! I'm a bit of a stickler Meeseeks, what about your short game? That just sounds like slavery with extra steps.")
                        )
                    )
                )),
                Layout.Row(row => row.Append(
                    Layout.Content(
                        Builder.GenerateParagraph(new Text("Ah, computer dating. It's like pimping, but you rarely have to use the phrase \"upside your head.\" You won't have time for sleeping, soldier, not with all the bed making you'll be doing. When I was first asked to make a film about my nephew, Hubert Farnsworth, I thought \"Why should I?\" Then later, Leela made the film. But if I did make it, you can bet there would have been more topless women on motorcycles. Roll film!"))
                    ),
                    Layout.Content(
                        Builder.GenerateParagraph(new Text("Something incredible is waiting to be known tesseract a mote of dust suspended in a sunbeam trillion cosmos realm of the galaxies. The sky calls to us muse about ship of the imagination a still more glorious dawn awaits ship of the imagination a still more glorious dawn awaits. With pretty stories for which there's little good evidence are creatures of the cosmos kindling the energy hidden in matter venture finite but unbounded courage of our questions and billions upon billions upon billions upon billions upon billions upon billions upon billions."))
                    ),
                    Layout.Content(
                        Builder.GenerateParagraph(new Text("Check cat door for ambush 10 times before coming in. Meow. Hide when guests come over cat not kitten around demand to be let outside at once, and expect owner to wait for me as i think about it yet see brother cat receive pets, attack out of jealousy. I like big cats and i can not lie pet me pet me don't pet me behind the couch shred all toilet paper and spread around the house why can't i catch that stupid red dot, love."))
                    )
                ))
            ));

            doc.Append(
                Builder.GenerateParagraph(
                    new Text("This document has been modified programmatically!"),
                    runStyle: () => new RunProperties(new Italic(), new Bold())
                ),
                layout,
                demo
            );

            return true;
        });
    }
}