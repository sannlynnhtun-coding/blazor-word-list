# Myanmar Spell Assistant (Blazor WASM)

A lightweight, privacy-focused, and high-performance Myanmar spelling assistant that runs entirely in your browser.

## 🙏 Credits & Acknowledgments
This project heavily utilizes the data and foundational logic from the [kanaung/wordlists](https://github.com/kanaung/wordlists) project. We wish to express our deep gratitude to the original authors and contributors of the Kanaung project for their invaluable contribution to Myanmar NLP and open-source wordlists.

---

## 🚀 Project Overview
Myanmar Spell Assistant is a **Client-Side Only** Blazor WebAssembly application. Unlike traditional spell checkers that send your sensitive text to a server, this app processes everything locally on your device. It's designed for speed, privacy, and ease of use.

### ✨ Enhanced Features Added
While building upon the Kanaung wordlists, I have implemented several modern features to provide a premium user experience:

1.  **Serverless Architecture (Blazor WASM)**: Ported the logic to C#/.NET 8 WebAssembly, enabling the spell checker to run offline after the initial load.
2.  **Modern UI with Tailwind CSS v4**: Built a sleek, Shadcn-inspired responsive interface using the latest Tailwind CSS v4 framework.
3.  **Real-Time Highlighting**: Implemented a "red-squiggly" system via JavaScript Interop that highlights errors as you type without breaking the editor's performance.
4.  **Interactive Floating Suggestions**: Developed a floating popup that appears when you click a misspelled word, offering instant corrections based on Levenshtein Distance.
5.  **Smart English Filtering**: Added logic to automatically detect and skip English/alphanumeric keywords, ensuring the checker stays focused on Myanmar spelling.
6.  **Optimized Memory Indexing**: Developed a `DictionaryLoaderService` that fetches static wordlist chunks and indexes them into a `HashSet<string>` for $O(1)$ lookup performance.
7.  **Instant Export**: One-click functionality to download your corrected text as a `.txt` file.
8.  **Automated Deployment**: Configured a specialized GitHub Actions workflow for seamless deployment to Vercel/Static Hosting.

## 🛠️ Tech Stack
- **Framework**: Blazor WebAssembly (.NET 8)
- **Styling**: Tailwind CSS v4 (with PostCSS & Autoprefixer)
- **Logic**: C# (Levenshtein Distance Algorithm)
- **Interop**: JavaScript (DOM manipulation & Event Delegation)
- **CI/CD**: GitHub Actions

## 📖 How to Run Locally
1. Clone the repository.
2. Ensure you have the .NET 8 SDK installed.
3. Install npm dependencies:
   ```bash
   cd BlazorWordList
   npm install
   ```
4. Build the CSS:
   ```bash
   npm run build:css
   ```
5. Run the project:
   ```bash
   dotnet watch
   ```

---
Built with ❤️ for the Myanmar Developer Community.
