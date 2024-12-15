import React from "react";
import api from "../../api/api";

const CarListingsDashboard = () => {
    const [listings, setListings] = useState([]);
  
    useEffect(() => {
      api.get("/CarListing")
        .then((res) => setListings(res.data))
        .catch((err) => console.error(err));
    }, []);
  
    const columns = [
      { field: "id", headerName: "ID", width: 150 },
      { field: "name", headerName: "Name", width: 200 },
      { field: "price", headerName: "Price", width: 100 },
    ];
  
    return <DataGrid rows={listings} columns={columns} pageSize={5} />;
  };
  
export default CarListingsDashboard;  