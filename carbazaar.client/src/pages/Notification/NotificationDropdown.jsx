import React, { useEffect, useState } from "react";
import api from "../../api/api";
import { Menu, MenuItem, Badge, Button } from "@mui/material";

const NotificationDropdown = () => {
  const [notifications, setNotifications] = useState([]);
  const [unreadCount, setUnreadCount] = useState(0);
  const [anchorEl, setAnchorEl] = useState(null);

  const fetchNotifications = async () => {
    const response = await api.get("/Notification/get-notifications", {
      params: { page: 1, pageSize: 10 },
    });
    setNotifications(response.data.items);
    setUnreadCount(response.data.items.filter((n) => !n.isRead).length);
  };

  const handleMarkAsRead = async () => {
    const unreadIds = notifications.filter((n) => !n.isRead).map((n) => n.id);
    await api.post("/Notification/mark-as-read", unreadIds);
    setUnreadCount(0);
  };

  useEffect(() => {
    fetchNotifications();
  }, []);

  return (
    <>
      <Badge badgeContent={unreadCount} color="secondary">
        <Button onClick={(e) => setAnchorEl(e.currentTarget)}>Notifications</Button>
      </Badge>
      <Menu
        anchorEl={anchorEl}
        open={Boolean(anchorEl)}
        onClose={() => setAnchorEl(null)}
      >
        {notifications.map((notification) => (
          <MenuItem key={notification.id}>
            {notification.message}
          </MenuItem>
        ))}
        <MenuItem onClick={handleMarkAsRead}>Mark All as Read</MenuItem>
      </Menu>
    </>
  );
};

export default NotificationDropdown;