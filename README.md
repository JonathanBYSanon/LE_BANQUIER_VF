# LE_BANQUIER_VF

**Le Banquier** is a game based on the popular TV show *Deal or No Deal.* The objective is for the player to finish with the highest possible amount between the 26 available prizes, either by keeping their selected briefcase or by accepting an offer from the banker. The game allows customization of the maximum prize amount and the player's name, making the experience more personal.

---

## Features

- **Exciting Gameplay:**
  - Choose a personal briefcase from 26 options.
  - Progress through multiple elimination rounds.
  - Receive and respond to banker offers strategically.
  - Make the final decision between keeping or switching briefcases.

- **Customization:**
  - Set the maximum prize amount.
  - Personalize the player's name.
  - Toggle sound effects and background music on/off.

- **User-Friendly Interface:**
  - Easy-to-navigate main menu with options to start a new game, view rules, adjust settings, and exit the game.
  - Interactive game board with clear visual indicators for progress.
  - Pop-up windows for key interactions and decisions.

- **Sound Effects:**
  - Engaging background music and sound effects for an immersive experience.
  - Option to mute/unmute audio.

---

## Installation Instructions

### For Code Access (Developers):
1. **Install .NET Framework 4.7.2:**
   - Download from Microsoft's official site.

2. **Clone the repository:**
   ```
   [git clone https://github.com/JonathanBYSanon/Le_Banquier_Vf.git](https://github.com/JonathanBYSanon/LE_BANQUIER_VF.git)
   cd le-banquier
   ```

3. **Run the project:**
   - Open the `.sln` file in Visual Studio 2019 or later.
   - Build and run the application.

---

### For Published Version (End Users):
1. **Download the game package.**
2. **Extract the ZIP file.**
3. **Run `Setup.exe` and follow the installation steps.**
4. **Launch "Le Banquier" from the Start Menu or desktop shortcut.**

---

## Usage Instructions

1. **Starting the Game:**
   - Click "Nouveau Jeu" on the main menu to start playing.

2. **Navigating the Menu:**
   - Choose from the following options:
     - **Nouveau Jeu (New Game):** Start a new game.
     - **Règles (Rules):** View detailed game rules.
     - **Paramètres (Settings):** Customize game options.
     - **Quitter (Quit):** Exit the application.

3. **Adjusting Game Settings:**
   - Enter the desired player name and maximum amount in the textboxes.
   - Toggle sound on/off using the provided button.
   - Click "Enregistrer" to save the settings.

4. **Playing the Game:**
   - Select and eliminate briefcases.
   - Decide whether to accept or refuse bank offers.
   - Win by keeping your briefcase or taking an offer.

---

## Project Structure

```
LeBanquier/
│-- Core/             # MVVM base and navigation logic
│-- Model/            # Data models used in the application
│-- Resource/         # Static resources and reusable UI elements
│   │-- Elements/     # Reusable visual elements (Briefcase, Host, Banker, etc.)
│   │-- Style/        # ResourceDictionaries for styling (colors, fonts, etc.)
│   │-- Image/        # Game images (logos, background, etc.)
│   │-- Sound/        # Audio files (sound effects, music)
│-- Service/          # Application logic services
│-- View/             # XAML files for UI components
│   │-- Popups/       # Views related to pop-up dialogs
│-- ViewModel/        # ViewModel layer for MVVM pattern
│   │-- Popups/       # ViewModels related to pop-up logic
│-- App.xaml          # Application startup and resource definitions
│-- MainWindow.xaml   # Main window of the application
```

### Key Services (Located in `Service/` folder):
- **Message Generator Service:** Generates in-game messages for the host.
- **Prize Calculation Service:** Generates 26 prizes based on the chosen max amount.
- **Briefcase Creation Service:** Creates 26 briefcases and assigns the shuffled prizes.

---

## Known Issues and Limitations

- **Settings Input:**
  Users should avoid entering invalid or unexpected values in the settings (e.g., non-numeric characters for the maximum amount). Ensure that only valid data is entered to prevent unexpected behavior.

---

## License

This project is distributed under [your license here] license.

---

## Credits

Special thanks to Thierno Souleymane, and Arthur Kameni

---

## Contact Information

For support or inquiries, reach out via:
📧 Email: jonathansanonpro@gmail.com
🌐 GitHub: [JonathanBYSanon](https://github.com/JonathanBYSanon)
