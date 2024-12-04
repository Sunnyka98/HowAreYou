INCLUDE girl.ink
INCLUDE dogowner.ink
INCLUDE mother.ink
INCLUDE runner.ink
INCLUDE homeless.ink

VAR days = 0
VAR girl = 0
VAR mother = 0
VAR runner = 0
VAR dogowner = 0
VAR homeless = 0
VAR isEnd = false 

+ Start
    -> start
    
== start ==
Es ist Tag: {days}
Wie jeden Nachmittag gehst du in den Park nahe deiner Wohnung. 

In der Mitte des Parks steht ein Springbrunnen mit einigen Bänken drum herrum. In der Nähe ein Spielplatz mit zwei Schaukeln und ein Kletterturm. 
Du setzt dich hin und beobachtest das Treiben. 
+ Jemand kommt näher 
    -> initGirl
    
== startAgain == 
~ days = days +1

Du beschliest das du genug Sonne für heute getankt hast und stehst auf. Auf dem Weg nach Hause verarbeitest du die Eindrücke des Tages und legst dich zuhause ins Bett. 
Gute Nacht 

 + Nächsten Tag starten 
{isEnd: 
    -> ende
- else: 
    -> start
}

== ende 
Das Abenteuer ist zuende 
-> END 












