== girl
{days: 
- 0: ->girlFirstLoop
}
->DONE

== girlFirstLoop
~ loops = loops + 1
Du kommst an eine Bank auf der ein junges Mädchen sitzt. Ihr Gesicht ist in ein zerfleddertes Notizbuch vertieft, in dem sie hektisch blättert. Gelegentlich hält sie inne, starrt auf eine Seite, murmelt etwas Unverständliches und schaut sich um, als suche sie etwas. Ihre Stirn ist gerunzelt, ihre Finger trommeln nervös auf den Buchdeckel. 

    + Du gehst auf sie zu und fragst: "Geht es dir gut?" 
        -> girlFirstLoop.positive
    + Du gehst an der Bank vorbei, ohne sie anzusprechen. 
        -> girlFirstLoop.negative
->DONE
= positive
Sie schaut auf, ihre Augen treffen deine. Einen Moment lang wirkt sie überrascht, doch dann verengen sich ihre Augen misstrauisch. "Spring nicht in eine Pfütze, dessen Boden du nicht siehst", sagt sie mit einem unerwartet ernsten Tonfall. Ehe du nachfragen kannst, senkt sie den Kopf und konzentiert sich wieder auf ihr Notizbuch, ohne dich weiter zu beachten. 
-> loop

= negative
    Während du auf ihrer Höhe bist, hörst du sie leise murmeln: "Wo ist das bloß?" Der Klang ihrer Stimme ist angespannt, fast verzweifelt. Du blickst kurz zurück, doch sie ist bereits wieder in ihr Notizbuch vertieft. 
-> loop
