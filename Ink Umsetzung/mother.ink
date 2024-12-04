== initMother
Nach einiger Zeit kommt eine junge Frau mit einem Kind auf deinen Sitzplatz zu. Sie setzt das kleine Mädchen auf die Bank und gibt ihr etwas zu essen. 

+ Aufstehen und gehen
    -> startAgain
+ Mutter beobachten 
    -> motherLook 
+ Wie geht es Ihnen? -> askMother
+ {dogowner > 1} Kennst du den Hundebesitzer, der hier manchmal hier ist? -> askMD
+ {girl > 2} Kennst du das Mädchen, welches hier manchmal ist? ->askMG
+ { homeless > 3 && days > 0} Kennst du den Obdachlosen, welcher manchmal hier ist? ->askMH

+ { runner > 4 } Kennst du den Jogger der hier manchmal vorbei kommt? -> askMR

= motherLook
TODO Aussehen der Mutter 
Eine junge Dame, vielleicht 20 Jahre alt mit einem Kind von ca 5 Jahren. 
-> motherEnd

= askMother 
{ mother <= 6: 
    TODO Mutter verschlossen 
    "Sie kennen mich nicht und ich glaube, dass sie es nicht interessiert."
-else: TODO Mutter öffnet sich 
}
-> motherEnd

= askMR
TODO Mutter nach Jogger fragen 
-> motherEnd

=askMH
TODO Mutter noch Obdachlosen fragen 
-> motherEnd

= askMG
TODO Mutter nach Mädchen fragen 
-> motherEnd

= askMD
TODO Mutter nach Hundebesitzer fragen 
-> motherEnd

= motherEnd
Das kleine Kind hat ihren Snack aufgegessen und sieht dich und dann ihre Mutter an. Ihre Mutter hebt sie von der Bank. Das Mädchen winkt dir noch bevor ihre Mutter und sie gehen. 
+ Du siehst in die Ferne und beobachtest.
    -> initHomeless

+ Nach Hause gehen 
    -> startAgain