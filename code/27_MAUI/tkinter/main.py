import tkinter as tk
from tkinter import messagebox

def say_hello():
    messagebox.showinfo("Status",
                        "Aus kleinem Anfang entspringen alle Dinge - Cicero")

window = tk.Tk()
window.title("Beispiel-GUI")
window.geometry('520x300')

hello_button = tk.Button(window, text="Ich bin keine einfache Anzeige ",
                        command=say_hello)
hello_button.pack(pady=20)

window.mainloop()
