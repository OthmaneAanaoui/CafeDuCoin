import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import axiosInstance from "../axiosConfig";

import {
  Container,
  Typography,
  Avatar,
  List,
  ListItem,
  ListItemText,
  ListItemAvatar,
  Box,
  Button,
} from "@mui/material";
import { FaGamepad } from "react-icons/fa";

const Detail = () => {
  const { id } = useParams();
  const [rentals, setRentals] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchRentals = async () => {
      try {
        const response = await axiosInstance.get(`/Game/games/${id}/history`);
        setRentals(response.data);
      } catch (error) {
        console.error("Error fetching rentals:", error);
      }
    };

    fetchRentals();
  }, [id]);
console.log(rentals)
  return (
    <Container maxWidth="lg" className="m-2 md:m-10 mt-24 p-2 md:p-6 bg-white rounded-3xl">
      <Box className="mb-10">
        <Box className="flex gap-4">
          <Box
            className="text-5xl rounded-lg p-5 hover:drop-shadow-xl"
            style={{ color: "#000", backgroundColor: "rgb(255, 245, 252)" }}
          >
            <FaGamepad />
          </Box>
          <Box>
            <Typography variant="h6" color="textSecondary">Détail</Typography>
            <Typography variant="h4" fontWeight="bold">Jeux</Typography>
            <Typography variant="h5" fontWeight="bold" color="textSecondary">Café du coin</Typography>
          </Box>
        </Box>
        <Button variant="outlined" color="secondary" onClick={() => navigate("/home")} sx={{ mt: 2 }}>
          Retour
        </Button>
      </Box>

      <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
        {rentals.map((rental) => (
          <ListItem key={rental.id}>
            <ListItemAvatar>
              <Avatar>
                <FaGamepad />
              </Avatar>
            </ListItemAvatar>
            <ListItemText 
              primary={rental.game.name} 
              secondary={`Loué par: ${rental.user.name} - Le: ${new Date(rental.rentalDate).toLocaleDateString()} - Retour: ${rental.returnDate ? new Date(rental.returnDate).toLocaleDateString() : "Pas encore retourné"}`} 
            />
          </ListItem>
        ))}
      </List>
    </Container>
  );
};

export default Detail;
