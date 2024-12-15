import React, { useEffect, useState } from "react";
import { DataGrid } from "@mui/x-data-grid";
import api from "../../api/api";

const UsersDashboard = () => {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    api.get("/Admin/get-users")
      .then((res) => setUsers(res.data))
      .catch((err) => console.error(err));
  }, []);

  const columns = [
    { field: "email", headerName: "Email", width: 200 },
  ];

  return <DataGrid rows={users} columns={columns} pageSize={5} />;
};

export default UsersDashboard;