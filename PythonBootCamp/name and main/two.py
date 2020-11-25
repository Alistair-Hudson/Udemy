#two.py

import one

print("TOP LEVEL IN TWO.PY")

one.func()

if __name__ == "__main__":
    print("TWO.PY IS BEIUNG RUN DIRECTLY")
else:
    print("TWO.PY HAS BEEN IMPORTED")