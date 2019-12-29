namespace October.Service

open System
open LiteDB.FSharp
open LiteDB

//open LiteDB

type Projet =
    { id : Guid
      nom : string
      duree : int
      note : string
      taux : decimal
    }

type Investissement = 
    { id : Guid
      idProjet : Guid
      date : DateTime
      montant : decimal
    }

type Remboursement =
    { id :Guid
      date : DateTime
    }

type RemboursementProjet=
    { id :Guid
      idProjet : Guid
      idRemboursement : Guid
      montant : decimal
    }



module ProjetService =
    let mapper = FSharpBsonMapper()
    use db = new LiteDatabase("simple.db", mapper)
    let AddProjet nom duree taux note date montant =
        let projets = db.GetCollection<Projet>("Projets")
        let newProjet =
            { id = Guid.NewGuid()
              nom = nom
              duree = duree
              taux = taux
              note = note
              }
        projets.Insert(newProjet)

        let investissements = db.GetCollection<Investissement>("Investissements")
        let newInvestissement =
            { id = Guid.NewGuid()
              idProjet = newProjet.id
              montant = montant
              date = date
            }
        investissements.Insert(newInvestissement)
        ()



    let get nom = 
        let projets = db.GetCollection<Projet>("Projets")
        let tryProjet = projets.Find (fun projet -> projet.nom = nom) |> Seq.tryHead
        match tryProjet with
            | None -> None
            | Some projet -> 
                let investissements = db.GetCollection<Investissement>("Investissements")
                let tryInvest = investissements.Find (fun inv -> inv.idProjet = projet.id) |> Seq.tryHead
                match tryInvest with
                    | None -> Some (projet.nom,None)
                    | Some invest -> Some (projet.nom, Some invest.montant)
                        
                
        


    let hello name =
        printfn "Hello %s" name
