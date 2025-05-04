import http.client
import json
from datetime import datetime

API_URL = "localhost:5097"

def create_pizza(name, description):
    conn = http.client.HTTPConnection(API_URL)
    payload = json.dumps({"nom": name, "description": description})
    headers = {'Content-Type': 'application/json'}
    
    conn.request("POST", "/pizza", body=payload, headers=headers)
    response = conn.getresponse()
    data = response.read().decode()
    
    print(f"CREATE: {data}")
    conn.close()
    return data

def get_pizzas():
    conn = http.client.HTTPConnection(API_URL)
    conn.request("GET", "/pizzas")
    response = conn.getresponse()
    data = response.read().decode()
    
    print(f"GET ALL: {data}")
    conn.close()
    return data

def get_pizza_by_id(pizza_id):
    conn = http.client.HTTPConnection(API_URL)
    conn.request("GET", f"/pizza/{pizza_id}")
    response = conn.getresponse()
    data = response.read().decode()
    
    print(f"GET ID {pizza_id}: {data}")
    conn.close()
    return data

def update_pizza(pizza_id, name, description):
    conn = http.client.HTTPConnection(API_URL)
    datestring = datetime.today().strftime('%b-%H-%M')
    payload = json.dumps({"name": name, "description": f"{description} #{datestring}"})
    headers = {'Content-Type': 'application/json'}
    
    conn.request("PUT", f"/pizza/{pizza_id}", body=payload, headers=headers)
    response = conn.getresponse()
    data = response.read().decode()
    
    print(f"UPDATE ID {pizza_id}: {data}")
    conn.close()
    return data

def delete_pizza(pizza_id):
    conn = http.client.HTTPConnection(API_URL)
    conn.request("DELETE", f"/pizza/{pizza_id}")
    response = conn.getresponse()
    data = response.read().decode()
    
    print(f"DELETE ID {pizza_id}: {data}")
    conn.close()
    return data

# Example usage
if __name__ == "__main__":
    print("Running CRUD operations...")
    
    create_pizza("Peperoni", "Une pizza au Peperoni")
    print("\n")
    pizzas = get_pizzas()
    last_id = (json.loads(pizzas)[3])["id"]
    print("\n")
    get_pizza_by_id(last_id)
    print("\n")
    update_pizza(last_id, "Peperoni Deluxe", "Une pizza avec plus de fromage")
    print("\n")
    get_pizzas()
    print("\n")
    # delete_pizza(last_id)
