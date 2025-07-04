interface User {
  id: number;
  name: string;
}

enum Role {
  Admin,
  Guest
}

type Result<T> = T | null;

function globalFunc(x: number): void {
  console.log(x);
}

const inline = (msg: string) => console.log(msg);

class Greeter {
  greet(name: string): string {
    return `Hello, ${name}`;
  }

  shout = (text: string) => {
    return text.toUpperCase();
  };
}

