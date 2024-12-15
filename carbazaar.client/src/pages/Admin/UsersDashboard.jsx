import React, { useEffect, useState } from "react";
import { DataGrid } from "@mui/x-data-grid";
import { Box, Typography, CircularProgress, Pagination } from "@mui/material";
import api from "../../api/api";

const UsersDashboard = () => {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);

  const fetchUsers = async (page) => {
    const params = {
      page: page,
      pageSize: 1
    }

    try {
      const response = await api.get(`/Admin/get-users`, { params });
      setUsers(response.data.items);
      setTotalPages(response.data.totalPages);
    } catch (err) {
      setError("Failed to fetch users. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers(page);
  }, [page]);

  const handlePageChange = (e, value) => {
    setPage(value);
  };

  const columns = [
    { field: "email", headerName: "Email", width: 200 },
  ];

  if (loading) {
    return (
      <Box sx={{ display: "flex", justifyContent: "center", mt: 5 }}>
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Typography color="error" variant="h6" sx={{ textAlign: "center", mt: 3 }}>
        {error}
      </Typography>
    );
  }

  return (
    <Box sx={{ p: 2 }}>
      <Typography variant="h4" sx={{ textAlign: "center", mb: 3 }}>
        Users Dashboard
      </Typography>
      <Box sx={{ height: 400, width: "100%" }}>
        <DataGrid
          rows={users}
          columns={columns}
          pageSize={5}
          disableSelectionOnClick
          paginationMode="server"
          rowCount={totalPages * 5}
          page={page - 1}
          onPageChange={(newPage) => setPage(newPage + 1)}
        />
      </Box>

      <Box sx={{ display: "flex", justifyContent: "center", mt: 3 }}>
        <Pagination
          count={totalPages}
          page={page}
          onChange={handlePageChange}
          color="primary"
          size="large"
        />
      </Box>
    </Box>
  );
};

export default UsersDashboard;