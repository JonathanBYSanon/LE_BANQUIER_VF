using LE_BANQUIER_VF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;



namespace LE_BANQUIER_VF.Service
{
    /// <summary>
    /// Service to generate messages for the game based on the current game state.
    /// </summary>
    public static class MessageGeneratorService
    {
        private static readonly Random _random = new Random();

        // Welcome messages for the player
        public static string GetWelcomeMessage(string playerName)
        {
            string[] messages = 
            {
                $"Bienvenue, {playerName} ! Prêt à affronter le banquier ? J'espère que tu as de la chance aujourd'hui ! Commence par choisir ta malette",
                $"Hello {playerName} ! Le banquier t'attend de pied ferme... Choisis ta mallette et prouve-lui que tu es imbattable !",
                $"Salut {playerName} ! As-tu le courage nécessaire pour affronter le banquier et repartir avec le jackpot ? La première étape est de choisir ta malette",
                $"Bienvenue dans l’arène, {playerName} ! Fais le bon choix et peut-être deviendras-tu riche... ou pas ! Commence par faire le bon choix de ta malette."
            };

            return messages[_random.Next(messages.Length)];
        }

        // Messages to confirm the player's choice of briefcase
        public static string GetBriefcaseSelectionMessage(string briefcaseNumber)
        {
            string[] messages = 
            {
                $"Hmm... la mallette numéro {briefcaseNumber}, intéressant. Es-tu certain de ce choix ?",
                $"La numéro {briefcaseNumber} ? Tu es sûr de toi ? Tu peux encore changer tu sais ?",
                $"{briefcaseNumber} ? Bon choix... ou peut-être pas ? Es-tu confiant(e) ?",
                $"Tu as choisi la {briefcaseNumber}... Mais est-ce vraiment la bonne ?"
            };

            return messages[_random.Next(messages.Length)];
        }

        // Message to react to the player's choice of briefcase
        public static string GetBriefcaseSelectionReaction(int briefcaseNumber,bool answer)
        {
            string[] trueMessages =
            {
                "Excellent choix ! Espérons que cette mallette soit celle du jackpot !",
                $"C’est noté, tu confirmes la mallette {briefcaseNumber}. Espérons que tu ne le regretteras pas !",
                $"Tu verrouilles ton choix sur la mallette {briefcaseNumber} ? Très bien, plus de retour en arrière !",
                $"Sage décision ! La mallette {briefcaseNumber} te suivra jusqu'au bout... Espérons qu'elle soit pleine à craquer !",
                $"La mallette {briefcaseNumber} est désormais tienne ! Espérons que tu as eu du flair cette fois-ci !",
                $"Pas de doutes ? La mallette {briefcaseNumber} est entre tes mains, voyons ce qu'elle vaut !"
            };

            string[] falseMessages =
            {
                "Tu hésites ? Peut-être qu'une autre mallette cache un meilleur trésor...",
                "Hum... tu veux changer ? Pas de problème, prends ton temps, la pression monte !",
                "Changement d'avis ? C'est prometteur mdr... Trouve la bonne cette fois !",
                $"Pas convaincu par la mallette {briefcaseNumber} ? Aucun souci, mais choisis judicieusement !",
                "Le doute s’installe ? Prends ton temps, mais n’oublie pas... la première intuition est souvent la bonne !",
                $"Tu abandonnes la mallette {briefcaseNumber} ? Espérons que ce ne soit pas une erreur fatale..."
            };

            return answer ? trueMessages[_random.Next(trueMessages.Length)] + "\nMaintenant c'est l'heure pour les choses sérieuses. Choisis ta première malette à éliminer." :
                                   falseMessages[_random.Next(falseMessages.Length)];
        }

        // Message to confirm the player's choice of briefcase to eliminate
        public static string GetOpenBriefcaseMessage(string briefcaseNumber)
        {
            string[] messages = 
            {
                $"Suspense... Que cache la mallette numéro {briefcaseNumber} ? J’espère pour toi que c’est du lourd !",
                $"Allons-nous briser tes rêves ou les réaliser avec cette mallette {briefcaseNumber} ?",
                $"Roulement de tambour... La mallette {briefcaseNumber} va révéler son secret !",
                $"Attention... la mallette {briefcaseNumber} à ouvrir. Prépare-toi à l'impact !"
            };

            return messages[_random.Next(messages.Length)];
        }

        // Message to react to the player eliminating a briefcase
        public static string GetOpenBriefcaseReaction(int revealedAmount, List<int> remainingAmounts)
        {
            string[] superMessages =
            {
                "Parfait ! ${0}. Mais quelle performance ? Tout se passe comme prévu !",
                "Excellent ! Le plus petit montant est sorti, le rêve continue !",
                "Tu viens d’éliminer le plus petit montant, c’est du bon boulot !",
                "Petit montant éliminé, le banquier commence à transpirer !",
                "Bien joué, tu peux souffler, ce n'était pas facile de l'avoir celle la."
            };

            string[] goodMessages =
            {
                "Pas mal du tout ! ${0} en moins, on avance dans le bon sens.",
                "C’est acceptable, ${0} ce n’était pas le jackpot de toute façon.",
                "Ça passe ! On garde l'essentiel en jeu.",
                "Un petit montant de côté, tu gères bien ton suspense.",
                "Petit à petit, tu fais le ménage des petites sommes."
            };

            string[] badMessages =
            {
                "Aïe... ${0} en moins, c'était quand même une belle somme.",
                "Ouch... Tu viens de perdre ${0}, ça commence à piquer !",
                "Pas top... ${0} s’en va, garde espoir mais reste prudent.",
                "Dommage, ${0} était une belle opportunité !",
                "Ça commence à faire mal... Il va falloir faire les bons choix maintenant."
            };

            string[] catastrophicMessages =
            {
                "Non... ${0} était le jackpot, quel coup dur !",
                "C'est la catastrophe... ${0} s'en va, le banquier jubile.",
                "Ouh là... ${0} envolé, ça sent pas bon du tout ! Le banquier doit se frotter les mains",
                "Coup dur... perdre ${0}, ça va peser dans la balance.",
                "Le plus gros montant a disparu... ça risque d’être compliqué maintenant."
            };
 
            int count = remainingAmounts.Count;

            int lowestRemaining = remainingAmounts.First();
            int highestRemaining = remainingAmounts.Last();
            double median = remainingAmounts[count / 2];

            if (revealedAmount == lowestRemaining)  // Super (meilleur scénario)
            {
                SoundManagerService.Instance.PlayEffect("GoodCaseSoundEffect.mp3");
                return string.Format(superMessages[_random.Next(superMessages.Length)], revealedAmount);
            }
            else if (revealedAmount == highestRemaining)  // Catastrophique (pire scénario)
            {
                SoundManagerService.Instance.PlayEffect("BadCaseSoundEffect.mp3");
                return string.Format(catastrophicMessages[_random.Next(catastrophicMessages.Length)], revealedAmount);
            }
            else if (revealedAmount < median)  // Pas mal (assez bon)
            {
                SoundManagerService.Instance.PlayEffect("GoodCaseSoundEffect.mp3");
                return string.Format(goodMessages[_random.Next(goodMessages.Length)], revealedAmount);
            }
            else  // Dure (montant élevé mais pas le pire)
            {
                SoundManagerService.Instance.PlayEffect("BadCaseSoundEffect.mp3");
                return string.Format(badMessages[_random.Next(badMessages.Length)], revealedAmount);
            }
        }

        public static string GetCancelBriefcaseEliminationMessage()
        {
            string[] cancelMessages =
            {
                "Tu changes d'avis ? Hésitation ou stratégie bien pensée ? Allez, choisis une autre mallette !",
                "Reculer pour mieux sauter ? Espérons que ce soit le bon choix ! Maintenant, prends une autre mallette.",
                "Ah, l’indécision... Mais après tout, c’est ton jeu ! Essaie encore, choisis une autre mallette.",
                "Finalement, tu préfères garder cette mallette... on verra si c'était une bonne idée. Sélectionne-en une autre !",
                "Annulation confirmée... Espérons que tu ne regrettes pas cette décision ! Allez, fais ton choix.",
                "Le doute s'installe, hein ? Espérons que cette mallette soit la bonne. Choisis-en une autre !",
                "Pas prêt à la lâcher ? Peut-être que tu sais quelque chose que nous ignorons ! Maintenant, fais un autre choix.",
                "Garder ou éliminer, telle est la question. Suspense... Choisis une autre mallette !"
            };

            Random random = new Random();
            return cancelMessages[random.Next(cancelMessages.Length)];
        }

        // Message to guide the player to the next round
        public static string GetNextRoundMessage()
        {
            string[] nextBriefcasePrompts =
            {
                "Quelle est la prochaine mallette que tu veux éliminer ?",
                "Alors, sur quelle mallette veux-tu tenter ta chance maintenant ?",
                "On avance ! Quelle mallette élimines-tu ensuite ?",
                "À ton tour ! Choisis la prochaine mallette à ouvrir.",
                "Le suspense continue... Quelle sera ta prochaine cible ?",
                "Réfléchis bien... Quelle mallette élimines-tu cette fois ?",
                "Un choix stratégique est nécessaire, quelle mallette t'inspire ?",
                "La tension monte... Quelle mallette veux-tu ouvrir maintenant ?"
            };

            Random random = new Random();
            return nextBriefcasePrompts[random.Next(nextBriefcasePrompts.Length)];
        }


        // Messages to inform the player of the banker's offer
        public static string GetOfferAcceptanceMessage(int offerAmount)
        {
            string[] messages = 
            {
                $"Le banquier te propose ${offerAmount}. C'est tentant, non ?",
                $"L'offre est de ${offerAmount}. Tu pourrais partir maintenant... ou tenter le destin.",
                $"Hmm... ${offerAmount}. C'est beaucoup... ou pas assez ? Décision difficile !",
                $"Le banquier est généreux aujourd’hui : ${offerAmount}. Vas-tu accepter ou prendre un risque ?"
            };

            return messages[_random.Next(messages.Length)];
        }

        // Messages to react to the player's decision to accept or refuse the banker's offer
        public static string GetPlayerDecisionReaction()
        {
            string[] messages =
            {
                "Oh ! Tu refuses l'offre ? Audacieux, j'aime ça ! Elimine donc la prochaine mallete vu que tu es si confiant(e)",
                "Pas de deal ? Attention, le banquier n'est pas tendre... Voyons si la prochaine malette te donne raison",
                "Tu prends des risques... J'espère que tu as fait le bon choix ! Quelle est la prochaine malette à eliminer ?",
                "Courageux ou imprudent ? On va le savoir très vite !  Quelle sera ta prochaine cible ?"
            };

            return messages[_random.Next(messages.Length)];
        }

        // Messages to inform the player to switch or keep their briefcase
        public static string GetSwitchBriefcaseMessage()
        {
            string[] messages = 
            {
                "C'est le moment crucial... Veux-tu échanger ta mallette ? La réponse pourrait changer ta vie !",
                "Attention, réfléchis bien... Garde-tu ta mallette ou changes-tu ?",
                "Échanger ou garder... Une décision à ne pas prendre à la légère !",
                "Un dernier choix : garde ta mallette ou prends-en une autre. Le destin est en jeu !"
            };

            return messages[_random.Next(messages.Length)];
        }

        // Messages to inform the player of the end of the game
        public static string GetEndGameMessage(int wonAmount, int lastBriefcaseAmount = 0, int playerBriefcaseAmount = 0, int lastOfferAmount = 0)
        {
            string[] messages;

            if (GameProgressService.Instance.Round > 24)
            {
                if (lastBriefcaseAmount > wonAmount)
                {
                    messages = new string[]
                    {
                        $"Oups ! Ta mallette contenait ${lastBriefcaseAmount}, mais tu repars avec ${wonAmount}... Le banquier a gagné cette fois !",
                        $"Aïe, tu avais ${lastBriefcaseAmount} dans ta mallette, mais tu as choisi de repartir avec ${wonAmount}. Un choix regrettable.",
                        $"Tu aurais pu repartir avec ${lastBriefcaseAmount}, mais tu as pris ${wonAmount}. Dur à avaler, hein ?"
                    };
                }
                else
                {
                    messages = new string[]
                    {
                        $"Bravo ! Tu as bien fait de garder cette mallette. ${wonAmount}, c'était le bon choix !",
                        $"Excellent flair ! Tu repars avec ${wonAmount} au lieu de ${lastBriefcaseAmount}. Félicitations !",
                        $"Bonne intuition ! Tu as choisi la bonne malette. Tu gagnes ${wonAmount}."
                    };
                }
            }
            else
            {
                if (wonAmount < playerBriefcaseAmount)
                {
                    messages = new string[]
                    {
                        $"Argh... Ta mallette contenait ${playerBriefcaseAmount}, mais tu as pris l'offre du banquier à ${wonAmount}. Mauvais choix !",
                        $"Dommage ! Ta mallette cachait ${playerBriefcaseAmount}, mais tu as accepté seulement ${wonAmount}.",
                        $"Tu n'as pas cru en ta chance... et tu as perdu ${playerBriefcaseAmount - wonAmount} en ne gardant pas ta mallette."
                    };
                }
                else
                {
                    messages = new string[]
                    {
                        $"Bien joué ! Tu as accepté l'offre de ${wonAmount} et évité une mallette à seulement ${playerBriefcaseAmount}.",
                        $"Stratégique ! Prendre ${wonAmount} était malin, ta mallette ne contenait que ${playerBriefcaseAmount}.",
                        $"Bonne décision ! Tu as gagné ${wonAmount} au lieu des misérables ${playerBriefcaseAmount} de ta mallette."
                    };
                }
            }

            SoundManagerService.Instance.PlayEffect("GoodGameSoundEffect.mp3");

            return messages[_random.Next(messages.Length)];
        }

        // Message to inform the player that the banker is calling
        public static string GetBankerCallMessage()
        {
            SoundManagerService.Instance.PlayEffect("PhoneRinging.mp3");
            return "Le banquier est au téléphone et il a une offre pour vous !";
        }
    }
}
